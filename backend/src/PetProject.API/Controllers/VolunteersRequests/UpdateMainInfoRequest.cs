using PetProject.Application.DTO;
using PetProject.Application.Volunteers.UpdateMainInfo;

namespace PetProject.API.Controllers.VolunteersRequests;

public record UpdateMainInfoRequest(
    string Name,
    string Surname,
    string? Patronymic,
    string Email,
    string Description,
    float Experience,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid id)
    {
        return new UpdateMainInfoCommand(
            id,
            new FullNameDto(Name, Surname, Patronymic),
            Email,
            Description,
            Experience,
            PhoneNumber);

    }
}
  