using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.PetVO;

public record HealthInformation
{
    public string Value { get;}

    private HealthInformation(string value)
    {
        Value = value;
    }

    public static Result<HealthInformation, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsRequired("HealthInformation");
        }
        if (value.Length > Constants.MAX_HIGH_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("HealthInformation");
        }
        
        return new HealthInformation(value);
    }
}