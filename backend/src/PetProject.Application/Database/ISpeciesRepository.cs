using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Species.ValueObjects;

namespace PetProject.Application.Database;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.Species.Species species, CancellationToken cancellationToken = default);
    Task<Guid> Save(Domain.Species.Species species, CancellationToken cancellationToken = default);
    Task<Guid> HardDelete(Domain.Species.Species species, CancellationToken cancellationToken);
    Task<Guid> SoftDelete(Domain.Species.Species species, CancellationToken cancellationToken);
    Task<Result<Domain.Species.Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default);
    
}