using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Volunteers;

public record Experience
{
    public float Value { get; }
    private Experience(float value)
    {
        Value = value;
    }

    public static Result<Experience, Error> Create(float value)
    {
     
        if (value < Constants.MIN_VALUE)
        {
            return Errors.General.ValueIsInvalid("Experience");
        }
        
        return new Experience(value);
    }
}