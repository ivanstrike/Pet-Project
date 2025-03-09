using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public record Color
{
    public string Value { get;}

    private Color(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure("Required fields are missing.");
        }
        
        var color = new Color(value);
        return Result.Success(color);
    }
}