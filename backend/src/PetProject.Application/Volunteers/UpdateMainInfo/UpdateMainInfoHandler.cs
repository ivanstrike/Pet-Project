using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.VolunteerContext.VolunteerVO;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private IVolunteersRepository _volunteersRepository;
    private IValidator<UpdateMainInfoCommand> _validator;
    private ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }


    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        var volunteerId = VolunteerId.Create(command.Id);
        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error.ToErrorList();
        }
        var email = Email.Create(command.Email).Value;
        var existingVolunteer = await _volunteersRepository.GetByEmail(email, cancellationToken);
        if (existingVolunteer.IsSuccess && existingVolunteer.Value.Id != volunteerResult.Value.Id)
        {
            return Errors.Volunteer.EmailAlreadyExists(email.Value).ToErrorList();
        }
        
        var fullName = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.Patronymic ).Value;
        
        var experience = Experience.Create(command.Experience).Value;
      
        var description = Description.Create(command.Description).Value;
        
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        volunteerResult.Value.UpdateMainInfo(
            fullName,
            email,
            description,
            experience,
            phoneNumber);
        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation(
            "Main information for volunteer with id:{Id} updated successfully",
            result);
        
        return result;
    }
}