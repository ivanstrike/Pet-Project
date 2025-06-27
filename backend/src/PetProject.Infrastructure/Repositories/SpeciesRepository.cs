using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Database;
using PetProject.Application.Volunteers;
using PetProject.Domain;
using PetProject.Domain.Shared;
using PetProject.Domain.Species;
using PetProject.Domain.Species.ValueObjects;

namespace PetProject.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpeciesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Add(Species species, CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id;
    }

    public async Task<Guid> Save(Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Attach(species);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id.Value;
    }
    public async Task<Guid> HardDelete(Species species, CancellationToken cancellationToken)
    {
        _dbContext.Species.Remove(species);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id.Value;
    }
    public async Task<Guid> SoftDelete(Species species, CancellationToken cancellationToken)
    {
        species.Delete();
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id.Value;
    }

    public async Task<Result<Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (species is null)
            return Errors.General.NotFound(id);

        return species;
    }
    
}