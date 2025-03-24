using Microsoft.AspNetCore.Mvc;
using PetProject.API.Extensions;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain.Shared;

namespace PetProject.API;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken) 
    {
        var result = await handler.Handle(
            request.VolunteerDto, 
            request.SocialNetworkDto, 
            request.RequisitesDto, 
            cancellationToken);

        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}