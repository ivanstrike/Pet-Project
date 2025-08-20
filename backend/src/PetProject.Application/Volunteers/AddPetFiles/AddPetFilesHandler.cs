using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Application.FileProvider;
using PetProject.Application.MessageQueues;
using PetProject.Application.Providers;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Domain.VolunteerContext.VolunteerVO;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Application.Volunteers.AddPetFiles;

public class AddPetFilesHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IFileProvider _fileProvider;
    private readonly IValidator<AddPetFilesCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    
    private readonly ILogger<AddPetFilesHandler> _logger;

    public AddPetFilesHandler(
        IFileProvider fileProvider, 
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetFilesCommand> validator,
        IUnitOfWork unitOfWork, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ILogger<AddPetFilesHandler> logger)
    {
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetFilesCommand command,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
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
            
            List<UploadFileData> uploadFilesData = [];
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);
                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var fileData = new UploadFileData(file.Content, new FileInfo(filePath.Value, BUCKET_NAME) );
                uploadFilesData.Add(fileData);
            }

            var petFiles = uploadFilesData
                .Select(f => f.FileInfo.FilePath)
                .Select(f => new PetFile(f))
                .ToList();
            
            var uploadResult = await _fileProvider.UploadFiles(uploadFilesData, cancellationToken);
            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(uploadFilesData.Select(f => f.FileInfo),  cancellationToken);
                return uploadResult.Error.ToErrorList();
            }
            
            petResult.UpdateFiles(petFiles);
            
            await _unitOfWork.SaveChanges(cancellationToken);
            transaction.Commit ();
            
            return petResult.Id.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Error while trying to add files to pet - {id}",
                command.PetId);
            
            transaction.Rollback();
            
            return Error.Failure("volunteer.pet.failure", "Error while trying to add files to pet").ToErrorList();
        }
        
    }
}