using PetProject.Application.DTO;
using PetProject.Application.Species.CreateSpecies;
using PetProject.Domain.Species;

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