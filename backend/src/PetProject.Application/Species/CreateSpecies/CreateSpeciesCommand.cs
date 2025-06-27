namespace PetProject.Application.Species.CreateSpecies;

public record CreateSpeciesCommand(
    string Name,
    IEnumerable<string> BreedsNames);