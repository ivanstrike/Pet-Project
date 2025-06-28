using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers.DeleteVolunteer;

public class DeleteVolunteerCommandValidator: AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}