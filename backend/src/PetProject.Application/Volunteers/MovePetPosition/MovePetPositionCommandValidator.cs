using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Application.Volunteers.MovePetPosition;

public class MovePetPositionCommandValidator: AbstractValidator<MovePetPositionCommand>
{
    public MovePetPositionCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.Position).MustBeValueObject(SerialNumber.Create);
    }
}