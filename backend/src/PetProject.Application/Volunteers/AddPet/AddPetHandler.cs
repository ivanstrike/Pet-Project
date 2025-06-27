using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.DTO;
using PetProject.Application.Extensions;
using PetProject.Domain;
using PetProject.Domain.Pets;
using PetProject.Domain.Pets.ValueObjects;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.Species;
using PetProject.Domain.Species.ValueObjects;
using PetProject.Domain.Volunteers;
using PetProject.Domain.Volunteers.Value_Objects;

namespace PetProject.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(IVolunteersRepository volunteersRepository,
        IValidator<AddPetCommand> validator,
        IUnitOfWork unitOfWork, ILogger<AddPetHandler> logger,
        ISpeciesRepository speciesRepository)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
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

            var speciesId = SpeciesId.Create(command.SpeciesId);
            var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
            if (speciesResult.IsFailure)
            {
                return speciesResult.Error.ToErrorList();
            }

            var breedId = BreedId.Create(command.BreedId);
            var breedResult = speciesResult.Value.Breeds.FirstOrDefault(s => s.Id == breedId);
            if (breedResult == null)
            {
                return Errors.General.NotFound(command.BreedId).ToErrorList();
            }

            var petId = PetId.NewPetId();
            var name = Name.Create(command.Name).Value;
            var description = Description.Create(command.Description).Value;
            var color = Color.Create(command.Color).Value;
            var healthInformation = HealthInformation.Create(command.HealthInformation).Value;
            var address = Address.Create(command.Address.Country,
                    command.Address.City,
                    command.Address.Street,
                    command.Address.HouseNumber)
                .Value;

            var size = Size.Create(command.Size.Height, command.Size.Weight).Value;
            var ownerPhone = PhoneNumber.Create(command.OwnerPhone).Value;
            var isNeutered = IsNeutered.Create(command.IsNeutered).Value;
            var birthDate = BirthDate.Create(command.BirthDate).Value;
            var isVaccinated = IsVaccinated.Create(command.IsVaccinated).Value;
            var status = Status.Create(command.Status).Value;
            List<Requisites> requisites = [ ];
            foreach (var requisite in command.Requisites)
            {
                var requisiteResult = Requisites.Create(requisite.Name,
                        requisite.Description)
                    .Value;

                requisites.Add(requisiteResult);
            }

            var pet = Pet.Create(petId,
                name,
                description,
                speciesId,
                breedId,
                color,
                healthInformation,
                address,
                size,
                ownerPhone,
                isNeutered,
                birthDate,
                isVaccinated,
                status,
                requisites);

            volunteerResult.Value.AddPet(pet.Value);

            await _unitOfWork.SaveChanges(cancellationToken);
            transaction.Commit();

            return pet.Value.Id.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to add pet to volunteer with ID: {VolunteerId}", command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("volunteer.pet.failure", "Error while trying to add pet to volunteer").ToErrorList();
        }
    }
}