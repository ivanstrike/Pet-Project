using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.SpeciesContext.SpeciesVO;

namespace PetProject.Application.SpeciesHandlers.CreateSpecies;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(x => x.Name).MustBeValueObject(Name.Create);
        RuleForEach(x => x.BreedsNames).MustBeValueObject(Name.Create);
    }
}