using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.PetVO;

public record IsNeutered
{
    public bool Value { get;}

    private IsNeutered(bool value)
    {
        Value = value;
    }

    public static Result<IsNeutered, Error> Create(bool value)
    {
        return new IsNeutered(value);
    }
}