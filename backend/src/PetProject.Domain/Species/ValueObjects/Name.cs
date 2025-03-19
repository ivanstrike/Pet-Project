using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Species;

public record Name
{
    public string Value { get; }
    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Errors.General.ValueIsRequired("Name");
        }
        if (value.Length > Constants.MAX_LOW_TEXT_LENGTH )
        {
            return Errors.General.ValueIsInvalid("Name");
        }
       
        return new Name(value);
    }
}