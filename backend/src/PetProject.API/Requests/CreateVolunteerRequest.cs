using PetProject.Application.DTO;
using PetProject.Domain;

namespace PetProject.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    VolunteerDto VolunteerDto,
    IEnumerable<SocialNetworkDto> SocialNetworkDto,
    IEnumerable<RequisitesDto> RequisitesDto)
{
    
}
    