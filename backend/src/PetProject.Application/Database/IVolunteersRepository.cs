using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;
using PetProject.Domain.Volunteers.Value_Objects;

namespace PetProject.Application.Database;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> HardDelete(Volunteer volunteer, CancellationToken cancellationToken);
    Task<Guid> SoftDelete(Volunteer volunteer, CancellationToken cancellationToken);
    Task<Result<Volunteer,Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
    Task<Result<Volunteer,Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);
    
}