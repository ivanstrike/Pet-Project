using CSharpFunctionalExtensions;

namespace PetProject.Domain.Species;

public class Breed : Shared.Entity<BreedId>
{
    // for ef
    private Breed(BreedId breedId) : base(breedId) {}
    public string Name { get; private set; } = string.Empty;

    public Breed(BreedId breedId, string name)
    :base(breedId)
    {
        Name = name;
    }

    public static Result Create( string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result.Failure("Name cannot be null or whitespace.");
        
        var breedId = BreedId.NewBreedId();
        var breed = new Breed(breedId, name);
        return Result.Success(breed);
    }
    
    
}