using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;
using PetProject.Domain.Volunteers.Value_Objects;

namespace PetProject.Application.Volunteers.DeleteVolunteer;

public class DeleteHardVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly ILogger<DeleteHardVolunteerHandler> _logger;

    public DeleteHardVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<DeleteVolunteerCommand> validator,
        ILogger<DeleteHardVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command,
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

        var result = await _volunteersRepository.HardDelete(volunteerResult.Value, cancellationToken);
        _logger.LogInformation(
            "Volunteer with id:{Id} deleted hard successfully",
            result);
        
        return result;
    }
}