using PetProject.Application.SpeciesHandlers.CreateSpecies;

namespace PetProject.API.Controllers.SpeciesRequests;

public record CreateSpeciesRequest(
    string Name,
    IEnumerable<string> BreedsNames)
{
    public CreateSpeciesCommand ToCommand()
    {
        return new CreateSpeciesCommand(Name, BreedsNames);
    }
}