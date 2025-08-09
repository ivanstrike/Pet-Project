using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Domain.VolunteerContext.VolunteerVO;

namespace PetProject.Application.Volunteers.MovePetPosition;

public class MovePetPositionHandler
{
    private readonly IValidator<MovePetPositionCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<MovePetPositionHandler> _logger;
    
    public MovePetPositionHandler(
        IVolunteersRepository volunteersRepository, 
        IValidator<MovePetPositionCommand> validator, 
        ILogger<MovePetPositionHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<int, ErrorList>> Handle(
        MovePetPositionCommand command,
        CancellationToken cancellationToken = default)
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

        var toSerialNumber = SerialNumber.Create(command.Position).Value;
        volunteerResult.Value.MovePet(petResult, toSerialNumber);;

        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        return petResult.SerialNumber.Value;

    }
}