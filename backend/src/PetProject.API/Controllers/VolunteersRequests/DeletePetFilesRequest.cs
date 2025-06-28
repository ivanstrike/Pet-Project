using PetProject.Application.Volunteers.DeletePetFiles;

namespace PetProject.API.Controllers.VolunteersRequests;

public record DeletePetFilesRequest(IEnumerable<string> FileNames)
{
    public DeletePetFilesCommand ToCommand(Guid volunteerId, Guid petId)
    {
        return new DeletePetFilesCommand(
            volunteerId,
            petId,
            FileNames);
    }
}