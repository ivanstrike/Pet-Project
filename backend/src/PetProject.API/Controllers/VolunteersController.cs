using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Controllers.Requests;
using PetProject.API.Extensions;
using PetProject.API.Response;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Application.Volunteers.UpdateRequisites;
using PetProject.Application.Volunteers.UpdateSocialMedia;
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
    
    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateMainInfoHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(Envelope.Ok(result.Value));
    }
    
    [HttpPut("{id:guid}/social-media")]
    public async Task<ActionResult<Guid>> UpdateSocialMedia(
        [FromRoute] Guid id,
        [FromBody] UpdateSocialMediaRequest request,
        [FromServices] UpdateSocialMediaHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(Envelope.Ok(result.Value));
    }
    
    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult<Guid>> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] UpdateRequisitesRequest request,
        [FromServices] UpdateRequisitesHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(Envelope.Ok(result.Value));
    }
}