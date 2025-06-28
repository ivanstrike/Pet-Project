using PetProject.Application.DTO;
using PetProject.Application.Volunteers.AddPetFiles;

namespace PetProject.API.Controllers.VolunteersRequests;

public record AddPetFilesRequest(IFormFileCollection Files)
{
    public AddPetFilesCommand ToCommand(Guid volunteerId, Guid petId, IEnumerable<CreateFileDto> fileNames)
    {
        return new AddPetFilesCommand(
            volunteerId,
            petId,
            fileNames);
    }
}