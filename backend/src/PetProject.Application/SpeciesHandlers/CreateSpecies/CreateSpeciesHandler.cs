using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared;
using PetProject.Domain.SpeciesContext;
using PetProject.Domain.SpeciesContext.SpeciesVO;

namespace PetProject.Application.SpeciesHandlers.CreateSpecies;

public class CreateSpeciesHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<CreateSpeciesHandler> _logger;
    private readonly IValidator<CreateSpeciesCommand> _validator;

    public CreateSpeciesHandler(
        ILogger<CreateSpeciesHandler> logger, 
        ISpeciesRepository repository,
        IUnitOfWork unitOfWork, 
        IValidator<CreateSpeciesCommand> validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSpeciesCommand command, 
        CancellationToken cancellationToken = default)
    {
        using var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return validationResult.ToErrorList();
            
            var speciesId = SpeciesId.NewSpeciesId();
            
            var name = Name.Create(command.Name).Value;
            
            var breeds = command.BreedsNames
                .Select(b => Breed.Create(BreedId.NewBreedId(), Name.Create(b).Value).Value)
                .ToList();
            
            var species = Species.Create(speciesId, name, breeds).Value;

            await _repository.Add(species, cancellationToken);
            
            await _unitOfWork.SaveChanges(cancellationToken);
            
            transaction.Commit();
            
            _logger.LogInformation(
                "Species {Id} created",
                species.Id.Value);
            
            return species.Id.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to create species");

            transaction.Rollback();

            return Error.Failure("species.failure", "Error while trying to create species").ToErrorList();
        }
        
    }
}