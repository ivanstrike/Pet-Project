using PetProject.Application.DTO;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid Id,
    FullNameDto FullName,
    string Email,
    string Description,
    float Experience,
    string PhoneNumber
)
{
    
}