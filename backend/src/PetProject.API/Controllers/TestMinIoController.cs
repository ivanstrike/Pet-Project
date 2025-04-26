using Microsoft.AspNetCore.Mvc;
using PetProject.API.Extensions;
using PetProject.API.Response;
using PetProject.Application.FileProvider;
using PetProject.Application.Volunteers.AddPet;

namespace PetProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestMinIoController : ControllerBase
{
    
    [HttpPost("/pet")]
    public async Task<ActionResult<Guid>> AddPet(
        //[FromRoute] Guid id,
        IFormFile file,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();
        var path = Guid.NewGuid().ToString();

        var fileData = new UploadFileData(stream, "photos", path);
        var result = await handler.Handle(fileData, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpGet("pet/{path}")]
    public async Task<ActionResult<Guid>> GetPet(
        [FromRoute] string path,
        [FromServices] GetPetHandler handler,
        CancellationToken cancellationToken)
    {
        var fileData = new FileData("photos", path);
        var result = await handler.Handle(fileData, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpDelete("pet/{path}")]
    public async Task<ActionResult<Guid>> DeletePet(
        [FromRoute] string path,
        [FromServices] DeletePetHandler handler,
        CancellationToken cancellationToken)
    {
        var fileData = new FileData("photos", path);
        var result = await handler.Handle(fileData, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Ok());
    }
}