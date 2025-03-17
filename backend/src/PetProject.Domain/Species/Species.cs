using CSharpFunctionalExtensions;

namespace PetProject.Domain.Species;

public class Species : Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
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
    
    public static Result Create(Name name, List<Breed> breeds)
    {
        var speciesId = SpeciesId.NewSpeciesId();
        var species = new Species(speciesId, name, breeds);
        return Result.Success(species);
    }
    
}