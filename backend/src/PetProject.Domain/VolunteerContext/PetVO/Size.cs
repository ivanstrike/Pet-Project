using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.PetVO;

public record Size
{
    public float Height { get; }
    public float Weight { get; }

    private Size(float height, float weight)
    {
        Height = height;
        Weight = weight;
    }

    public static Result<Size, Error> Create(float height, float weight)
    {
        if (weight <= Constants.MIN_VALUE)
        {
            return Errors.General.ValueIsInvalid("Weight");
        }
        
        if (height <= Constants.MIN_VALUE)
        {
            return Errors.General.ValueIsInvalid("Height");
        }
        
        return new Size(height, weight);
        
    }
}