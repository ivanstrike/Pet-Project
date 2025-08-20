using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;

namespace PetProject.Application.Volunteers.UpdateRequisites;

public class UpdateRequisitesCommandValidator: AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.Requisites)
            .MustBeValueObject(x => Requisites.Create(x.Name, x.Description));
    }
}