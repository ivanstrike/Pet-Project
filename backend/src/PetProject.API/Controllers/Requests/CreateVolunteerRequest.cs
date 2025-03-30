using PetProject.Application.DTO;
using PetProject.Application.Volunteers.CreateVolunteer;

namespace PetProject.API.Controllers.Requests;

public record CreateVolunteerRequest(
    string Name,
    string Surname,
    string? Patronymic,
    string Email,
    string Description,
    float Experience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworkDto,
    IEnumerable<RequisitesDto> RequisitesDto)
{
    public CreateVolunteerCommand ToCommand()
    {
        return new CreateVolunteerCommand(
            new FullNameDto(Name, Surname, Patronymic),
            Email,
            Description,
            Experience,
            PhoneNumber,
            SocialNetworkDto,
            RequisitesDto);
    }
}
    
    