using PetProject.Application.DTO;
using PetProject.Application.Volunteers.UpdateRequisites;

namespace PetProject.API.Controllers.VolunteersRequests;

public record UpdateRequisitesRequest(IEnumerable<RequisitesDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid id)
    {
        return new UpdateRequisitesCommand(
            id, Requisites.Select(x => new RequisitesDto(x.Name, x.Description)).ToList());
    }
}