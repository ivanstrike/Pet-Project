using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public record Colour
{
    public string Value { get;}

    private Colour(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure("Required fields are missing.");
        }
        
        var colour = new Colour(value);
        return Result.Success(colour);
    }
}