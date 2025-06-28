using System.Reflection;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Database;
using PetProject.Application.Volunteers;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext;
using PetProject.Domain.VolunteerContext.VolunteerVO;

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

    public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Attach(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Guid> HardDelete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Remove(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Guid> SoftDelete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        volunteer.Delete();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound(id);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(m => m.Email == email, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }
}