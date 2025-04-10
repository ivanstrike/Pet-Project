using PetProject.Application.DTO;
using PetProject.Application.Volunteers.UpdateRequisites;
using PetProject.Domain.Shared.Value_Objects;

namespace PetProject.API.Controllers.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisitesDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid id)
    {
        return new UpdateRequisitesCommand(
            id, Requisites.Select(x => new RequisitesDto(x.Name, x.Description)).ToList());
    }
}