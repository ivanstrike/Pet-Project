using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.SpeciesContext.SpeciesVO;

namespace PetProject.Domain.SpeciesContext;

public class Species : Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    private bool _isDeleted = false;
    // for ef
    private Species() : base(SpeciesId.Empty()) {}
    private Species(SpeciesId speciesId) : base(speciesId) {}
    
    public Species(SpeciesId speciesId, Name name, List<Breed> breeds)
    :base(speciesId)
    {
        Name = name;
        _breeds = breeds;
    }
    public Name Name { get; private set; } = default!;
    public IReadOnlyList<Breed> Breeds => _breeds;
    
    public void Delete()
    {
        if (!_isDeleted)
        {
            _isDeleted = true;
            DeletePets();
        }
    }

    public void DeletePets()
    {
        foreach (var breed in _breeds)
        {
            breed.Delete();
        }
    }

    public void RestorePets()
    {
        foreach (var breed in _breeds)
        {
            breed.Restore();
        }
    }

    public void Restore()
    {
        if (_isDeleted)
        {
            _isDeleted = false;
            RestorePets();
        }
    }
    public static Result<Species, Error> Create(
        SpeciesId speciesId, 
        Name name, 
        List<Breed> breeds)
    {
        var species = new Species(speciesId, name, breeds);
        return species;
    }
    
}