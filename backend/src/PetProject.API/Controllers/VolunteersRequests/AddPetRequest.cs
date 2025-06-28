using PetProject.Application.DTO;
using PetProject.Application.Volunteers.AddPet;

namespace PetProject.API.Controllers.VolunteersRequests;

public record AddPetRequest(
    string Name,
    string Description,
    Guid SpeciesId,
    Guid BreedId,
    string Color,
    string HealthInformation,
    AddressDto Address,
    SizeDto Size,
    string OwnerPhone,
    bool IsNeutered,
    string BirthDate,
    bool IsVaccinated,
    string Status,
    IEnumerable<RequisitesDto> Requisites)
{
    public AddPetCommand ToCommand(Guid id)
    {
        return new AddPetCommand(
            id,
            Name,
            Description,
            SpeciesId,
            BreedId,
            Color,
            HealthInformation,
            Address,
            Size,
            OwnerPhone,
            IsNeutered,
            BirthDate,
            IsVaccinated,
            Status,
            Requisites);
    }
}