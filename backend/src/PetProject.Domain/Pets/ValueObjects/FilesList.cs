namespace PetProject.Domain.Pets.ValueObjects;

public record FilesList
{
    private FilesList()
    {
        
    }

    public FilesList(IEnumerable<PetFile> files)
    {
        Files = files.ToList();
    }
    public IReadOnlyList<PetFile> Files { get; }
}