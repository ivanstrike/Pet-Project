using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Pets.ValueObjects;

public record BirthDate
{
    public DateOnly Value { get; }

    private BirthDate(DateOnly value)
    {
        Value = value;
    }

    public static Result<BirthDate, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.ValueIsRequired("BirthDate");
        }
        
        if (!DateOnly.TryParse(value, out var date))
        {
            return Errors.General.ValueIsInvalid("BirthDate");
        }
        
        return new BirthDate(date);
    }
}