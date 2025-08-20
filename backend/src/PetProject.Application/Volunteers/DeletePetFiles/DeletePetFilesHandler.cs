using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Domain.VolunteerContext.VolunteerVO;

namespace PetProject.Application.Volunteers.DeletePetFiles;

public class DeletePetFilesHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeletePetFilesCommand> _validator;
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeletePetFilesHandler> _logger;

    public DeletePetFilesHandler(IFileProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        IValidator<DeletePetFilesCommand> validator,
        IUnitOfWork unitOfWork,
        ILogger<DeletePetFilesHandler> logger)
    {
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(DeletePetFilesCommand command,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrorList();
            }
            
            var volunteerResult = await _volunteersRepository
                .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            var petResult = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id.Value == command.PetId);
            if (petResult is null)
                return Errors.General.NotFound(command.PetId).ToErrorList();

            List<FileData> deleteFilesData = [];

            List<PetFile> petFiles = [];

            foreach (var file in petResult.Files)
            {
                if (command.FileNames.Contains(file.PathToStorage.Value))
                    deleteFilesData.Add(new FileData(file.PathToStorage.Value, BUCKET_NAME));
                else
                    petFiles.Add(file);
            }

            petResult.DeleteFiles(petFiles);

            await _unitOfWork.SaveChanges(cancellationToken);

            var deleteResult = await _fileProvider.DeleteFiles(deleteFilesData, cancellationToken);
            if (deleteResult.IsFailure)
                return deleteResult.Error.ToErrorList();

            transaction.Commit();

            return petResult.Id.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Error while trying to delete files from pet - {id}",
                command.PetId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.failure", "Error while trying to delete files from pet").ToErrorList();
        }
    }
}