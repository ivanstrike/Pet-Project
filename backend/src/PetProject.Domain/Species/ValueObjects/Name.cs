using CSharpFunctionalExtensions;
namespace PetProject.Domain.Species;

public record Name
{
    public string Value { get; }
    private Name(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Result.Failure("Name can't be empty.");
        }
        
        var name = new Name(value);
        return Result.Success(name);
    }
}