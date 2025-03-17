using CSharpFunctionalExtensions;

namespace PetProject.Domain.Species;

public class Breed : Shared.Entity<BreedId>
{
    // for ef
    private Breed() : base(BreedId.Empty()) {}
    private Breed(BreedId breedId) : base(breedId) {}
    public Name Name { get; private set; } = default!;

    public Breed(BreedId breedId, Name name)
    :base(breedId)
    {
        Name = name;
    }

    public static Result Create(Name name)
    {
        var breedId = BreedId.NewBreedId();
        var breed = new Breed(breedId, name);
        return Result.Success(breed);
    }
    
    
}