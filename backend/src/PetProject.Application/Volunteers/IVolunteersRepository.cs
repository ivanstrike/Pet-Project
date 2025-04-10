using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
    Task<Result<Volunteer,Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);
}