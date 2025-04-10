using PetProject.Application.DTO;
using PetProject.Domain.Volunteers;

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