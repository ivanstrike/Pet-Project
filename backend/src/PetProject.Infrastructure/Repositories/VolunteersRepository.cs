using System.Reflection;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;

namespace PetProject.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer,Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        
        if (volunteer == null)
            return Errors.General.NotFound(id);

        return volunteer;
    }
}