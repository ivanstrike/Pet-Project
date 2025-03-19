using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record Color
{
    public string Value { get;}

    private Color(string value)
    {
        Value = value;
    }

    public static Result<Color, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsRequired("Color");
        }
        if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Color");
        }
        
        return new Color(value);
    }
}