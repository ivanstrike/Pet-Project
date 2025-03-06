using CSharpFunctionalExtensions;

namespace PetProject.Domain.Species;

public class Species : Shared.Entity<SpeciesId>
{
    // for ef
    private Species(SpeciesId speciesId) : base(speciesId) {}
    
    private Species(SpeciesId speciesId, string name, List<Breed> breeds)
    :base(speciesId)
    {
        Name = name;
        Breeds = breeds;
    }
    public SpeciesId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public List<Breed> Breeds { get; private set; } = [];
    
    public static Result Create(SpeciesId speciesId, string name, List<Breed> breeds)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            return Result.Failure("Name cannot be null or whitespace.");
        
        var species = new Species(speciesId, name, breeds);
        return Result.Success(species);
    }
    
}