using PetProject.Application.DTO;

namespace PetProject.Application.Volunteers.AddPetFiles;

public record AddPetFilesCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files);