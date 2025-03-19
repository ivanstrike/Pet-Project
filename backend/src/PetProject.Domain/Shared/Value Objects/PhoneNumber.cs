using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Volunteers;

public record PhoneNumber
{
    public string Value { get; }
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Errors.General.ValueIsRequired("PhoneNumber");
        }
        if (value.Length >= Constants.MAX_PHONE_NUMBER_LENGTH )
        {
            return Errors.General.ValueIsInvalid("PhoneNumber");
        }
        
        return new PhoneNumber(value);
    }
}