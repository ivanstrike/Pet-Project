namespace PetProject.Application.Volunteers.DeletePetFiles;

public record DeletePetFilesCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> FileNames);