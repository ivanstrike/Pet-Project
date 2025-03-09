using CSharpFunctionalExtensions;

namespace PetProject.Domain.Volunteers;

public record PhoneNumber
{
    public string Value { get; }
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Result.Failure("PhoneNumber is missing.");
        }
        
        var phoneNumber = new PhoneNumber(value);
        return Result.Success(phoneNumber);
    }
}