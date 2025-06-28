namespace PetProject.Domain.VolunteerContext.PetVO;

public record PetFile
{
    public PetFile(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    public FilePath PathToStorage { get; }
}