using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.UpdateSocialMedia;

public class UpdateSocialMediaCommandValidator: AbstractValidator<UpdateSocialMediaCommand>
{
    public UpdateSocialMediaCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.SocialNetworks)
            .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
    }
}