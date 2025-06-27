using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Pets.ValueObjects;

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