using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public record Status
{
    public string Value { get; }

    private Status(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Result.Failure("Status is missing.");
        }
        
        var status = new Status(value);
        return Result.Success(status);
    }
}