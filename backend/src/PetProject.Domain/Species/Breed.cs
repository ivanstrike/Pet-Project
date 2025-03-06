using CSharpFunctionalExtensions;

namespace PetProject.Domain.Species;

public class Breed : Shared.Entity<BreedId>
{
    // for ef
    private Breed(BreedId breedId) : base(breedId) {}
    
    public BreedId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private Breed(BreedId breedId, string name)
    :base(breedId)
    {
        Name = name;
    }

    public static Result Create(BreedId breedId, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result.Failure("Name cannot be null or whitespace.");
        var breed = new Breed(breedId, name);
        return Result.Success(breed);
    }
    
    
}