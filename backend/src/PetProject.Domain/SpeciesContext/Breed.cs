using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.SpeciesContext.SpeciesVO;

namespace PetProject.Domain.SpeciesContext;

public class Breed : Shared.Entity<BreedId>
{
    private bool _isDeleted = false;
    // for ef
    private Breed() : base(BreedId.Empty()) {}
    private Breed(BreedId breedId) : base(breedId) {}
    public Name Name { get; private set; } = default!;

    public Breed(BreedId breedId, Name name)
    :base(breedId)
    {
        Name = name;
    }
    public void Delete()
    {
        if (!_isDeleted)
            _isDeleted = true;
    }

    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
    }
    public static Result<Breed, Error> Create(BreedId breedId, Name name)
    {
        var breed = new Breed(breedId, name);
        return breed;
    }
    
    
}