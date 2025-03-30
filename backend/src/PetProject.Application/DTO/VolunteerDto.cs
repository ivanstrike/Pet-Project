using PetProject.Domain.Volunteers;

namespace PetProject.Application.DTO;

public record VolunteerDto(
    string Name,
    string Surname,
    string? Patronymic,
    string Email,
    string Description,
    float Experience,
    string PhoneNumber);
