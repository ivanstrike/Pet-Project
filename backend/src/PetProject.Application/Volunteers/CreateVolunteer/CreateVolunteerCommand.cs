using PetProject.Application.DTO;

namespace PetProject.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    float Experience,
    string PhoneNumber,
    IEnumerable<SocialNetworkDto> SocialNetworkDto,
    IEnumerable<RequisitesDto> RequisitesDto)
{
    
}
    