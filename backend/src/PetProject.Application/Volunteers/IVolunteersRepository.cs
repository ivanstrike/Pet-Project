using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;

namespace PetProject.Infrastructure.Repositories;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
}