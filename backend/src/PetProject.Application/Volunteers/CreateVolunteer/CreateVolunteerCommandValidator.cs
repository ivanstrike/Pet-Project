using FluentValidation;
using PetProject.Application.DTO;
using PetProject.Application.Validation;
using PetProject.Domain;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerCommandValidator: AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.FullName).SetValidator(new FullNameDtoValidator());
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Experience).MustBeValueObject(Experience.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleForEach(c => c.SocialNetworkDto).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
        RuleForEach(c => c.RequisitesDto).MustBeValueObject(x => Requisites.Create(x.Name, x.Description));
        
    }
}

