namespace PetProject.Application.SpeciesHandlers.CreateSpecies;

public record CreateSpeciesCommand(
    string Name,
    IEnumerable<string> BreedsNames);