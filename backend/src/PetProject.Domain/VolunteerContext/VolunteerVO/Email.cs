using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.VolunteerVO;

public record Email
{
    public string Value { get; }
    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) )
        {
            return Errors.General.ValueIsRequired("Email");
        }
        
        if (value.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Email");
        }
        
        return new Email(value);
    }
}