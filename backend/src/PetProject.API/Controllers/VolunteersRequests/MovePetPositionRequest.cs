using PetProject.Application.Volunteers.MovePetPosition;

namespace PetProject.API.Controllers.VolunteersRequests;

public record MovePetPositionRequest(int Position)
{
    public MovePetPositionCommand ToCommand(Guid volunteerId, Guid petId) =>
        new MovePetPositionCommand(volunteerId, petId, Position);
}