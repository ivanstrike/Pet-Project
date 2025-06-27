using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.Volunteers;
using PetProject.Domain.Volunteers.Value_Objects;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoCommandValidator: AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.FullName).SetValidator(new FullNameDtoValidator());
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Experience).MustBeValueObject(Experience.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}