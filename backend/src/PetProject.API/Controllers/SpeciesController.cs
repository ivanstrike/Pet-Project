using Microsoft.AspNetCore.Mvc;
using PetProject.API.Controllers.SpeciesRequests;
using PetProject.API.Extensions;
using PetProject.API.Response;
using PetProject.Application.Species.CreateSpecies;

namespace PetProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SpeciesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateSpeciesHandler handler,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await handler.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return result.Error.ToResponse();
        
        return Ok(Envelope.Ok(result.Value));
    }
}