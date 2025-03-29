using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Controllers.Requests;
using PetProject.API.Extensions;
using PetProject.API.Response;
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
        var command = request.ToCommand();
        var result = await handler.Handle(command, cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(Envelope.Ok(result.Value));
    }
}