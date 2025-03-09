using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public class OwnerPhone
{
    public string Value { get; }
    private OwnerPhone(string value)
    {
        Value = value;
    }

    public static Result Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Result.Failure("OwnerPhone is missing.");
        }
        
        var ownerPhone = new OwnerPhone(value);
        return Result.Success(ownerPhone);
    }
}