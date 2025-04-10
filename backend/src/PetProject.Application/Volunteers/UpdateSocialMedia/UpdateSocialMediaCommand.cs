using PetProject.Application.DTO;

namespace PetProject.Application.Volunteers.UpdateSocialMedia;

public record UpdateSocialMediaCommand(Guid Id, IEnumerable<SocialNetworkDto> SocialNetworks);