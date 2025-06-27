using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Species;
using PetProject.Domain.Species.ValueObjects;

namespace PetProject.Application.Species.CreateSpecies;

public class CreateSpeciesHandler
{
    private ISpeciesRepository _repository;
    private ILogger<CreateSpeciesHandler> _logger;
    private IValidator<CreateSpeciesCommand> _validator;

    public CreateSpeciesHandler(
        ISpeciesRepository repository, 
        ILogger<CreateSpeciesHandler> logger, 
        IValidator<CreateSpeciesCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSpeciesCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var speciesId = SpeciesId.NewSpeciesId();
        var name = Name.Create(command.Name).Value;
        var breeds = command.BreedsNames
            .Select(b => Breed.Create(BreedId.NewBreedId(), Name.Create(b).Value).Value)
            .ToList();
        
        var species = Domain.Species.Species.Create(speciesId, name, breeds).Value;
        await _repository.Add(species, cancellationToken);
        _logger.LogInformation(
            "Species {Id} created",
            species.Id.Value);
        return species.Id.Value;
    }
}