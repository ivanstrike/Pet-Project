using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public record Description
{
    public string Value { get;}

    private Description(string value)
    {
        Value = value;
    }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsRequired("Description");
        }
        if (value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Description");
        }
        
        return new Description(value);
    }
}