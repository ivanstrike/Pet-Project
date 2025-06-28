using PetProject.Application.DTO;

namespace PetProject.Application.Volunteers.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
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
    IEnumerable<RequisitesDto> Requisites
);
