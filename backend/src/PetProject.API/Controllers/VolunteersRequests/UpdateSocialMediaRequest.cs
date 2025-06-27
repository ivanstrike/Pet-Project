using PetProject.Application.DTO;
using PetProject.Application.Volunteers.UpdateSocialMedia;

namespace PetProject.API.Controllers.VolunteersRequests;

public record UpdateSocialMediaRequest(IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialMediaCommand ToCommand(Guid id) =>
        new(id, SocialNetworks.Select(x => new SocialNetworkDto(x.Name, x.Link)).ToList());
}