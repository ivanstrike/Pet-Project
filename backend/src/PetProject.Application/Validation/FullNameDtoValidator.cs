using FluentValidation;
using PetProject.Application.DTO;
using PetProject.Domain.VolunteerContext.VolunteerVO;

namespace PetProject.Application.Validation;

public class FullNameDtoValidator: AbstractValidator<FullNameDto>
{
    public FullNameDtoValidator()
    {
        RuleFor(f => new { f.Name, f.Surname, f.Patronymic })
            .MustBeValueObject(x => 
                FullName.Create(x.Name, x.Surname, x.Patronymic));
    }
}