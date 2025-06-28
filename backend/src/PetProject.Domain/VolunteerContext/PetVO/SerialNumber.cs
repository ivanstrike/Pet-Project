using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.PetVO;

public record SerialNumber
{
    public int Value { get; }
    public static SerialNumber First = new(1);
    
    private SerialNumber(int value) => Value = value;

    public static Result<SerialNumber, Error> Create(int number)
    {
        if (number <= 0)
            return Errors.General.ValueIsInvalid("serial number");
        return new SerialNumber(number);
    }
}