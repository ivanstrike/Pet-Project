using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Pets.ValueObjects;

public record Status
{
    public string Value { get; }

    private Status(string value)
    {
        Value = value;
    }

    public static Result<Status, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Errors.General.ValueIsRequired("Status");
        }
        if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Status");
        }
        
        return new Status(value);
    }
}