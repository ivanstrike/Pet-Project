using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.SpeciesContext;
using PetProject.Domain.SpeciesContext.SpeciesVO;

namespace PetProject.Application.Database;

public interface ISpeciesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);
    Task<Guid> Save(Species species, CancellationToken cancellationToken = default);
    Task<Guid> HardDelete(Species species, CancellationToken cancellationToken);
    Task<Guid> SoftDelete(Species species, CancellationToken cancellationToken);
    Task<Result<Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default);

}