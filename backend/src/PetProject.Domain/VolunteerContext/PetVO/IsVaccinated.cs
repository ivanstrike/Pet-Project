using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.PetVO;

public record IsVaccinated
{
    public bool Value { get;}

    private IsVaccinated(bool value)
    {
        Value = value;
    }

    public static Result<IsVaccinated, Error> Create(bool value)
    {
        return new IsVaccinated(value);
    }
}