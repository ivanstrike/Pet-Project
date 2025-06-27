using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.Volunteers;
using PetProject.Domain.Volunteers.Value_Objects;

namespace PetProject.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        var email = Email.Create(command.Email).Value;
        var existingVolunteer = await _volunteersRepository.GetByEmail(email, cancellationToken);
        
        if (existingVolunteer.IsSuccess)
        {
            return Errors.Volunteer.EmailAlreadyExists(email.Value).ToErrorList();
        }
        
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var fullName = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.Patronymic ).Value;
        
        var experience = Experience.Create(command.Experience).Value;
      
        var description = Description.Create(command.Description).Value;
        
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        

        List<SocialNetwork> socialNetworks = [];
        foreach (var socialNetwork in command.SocialNetworks)
        {
            var socialNetworkResult = SocialNetwork.Create(
                socialNetwork.Name, 
                socialNetwork.Link)
                .Value;
            socialNetworks.Add(socialNetworkResult);
        }

        List<Requisites> requisites = [];
        foreach (var requisite in command.Requisites)
        {
            var requisiteResult = Requisites.Create(
                requisite.Name, 
                requisite.Description)
                .Value;
            requisites.Add(requisiteResult);
        }
        
        var volunteer = Volunteer.Create(
            volunteerId, 
            fullName, 
            email, 
            description, 
            experience, 
            phoneNumber, 
            socialNetworks, 
            requisites);
        
        await _volunteersRepository.Add(volunteer.Value, cancellationToken);
        _logger.LogInformation(
            "Volunteer {Id} created",
            volunteer.Value.Id.Value);
        return volunteer.Value.Id.Value;
    }
    
}