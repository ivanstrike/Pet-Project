

using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PetProject.Application.DTO;
using PetProject.Application.Validation;
using PetProject.Domain;
using PetProject.Domain.Pets.ValueObjects;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.Species;
using PetProject.Domain.Species.ValueObjects;

namespace PetProject.Application.Volunteers.AddPet;

public class AddPetCommandValidator: AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.Name).MustBeValueObject(Name.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.Color).MustBeValueObject(Color.Create);
        RuleFor(c => c.HealthInformation).MustBeValueObject(HealthInformation.Create);
        RuleFor(c => c.Address).MustBeValueObject(x => Address.Create(x.Country, x.City, x.Street, x.HouseNumber));
        RuleFor(c => c.Size).MustBeValueObject(x => Size.Create(x.Height, x.Weight));
        RuleFor(c => c.OwnerPhone).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.IsNeutered).MustBeValueObject(IsNeutered.Create);
        RuleFor(c => c.BirthDate).MustBeValueObject(BirthDate.Create);
        RuleFor(c => c.IsVaccinated).MustBeValueObject(IsVaccinated.Create);
        RuleFor(c => c.Status).MustBeValueObject(Status.Create);
        RuleForEach(c => c.Requisites).MustBeValueObject(x => Requisites.Create(x.Name, x.Description));
    }
}