using CSharpFunctionalExtensions;

namespace PetProject.Domain.Volunteers;

public record Email
{
    public string Value { get; }
    private Email(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Result.Failure("Email is missing.");
        }
        
        var email = new Email(value);
        return Result.Success(email);
    }
}