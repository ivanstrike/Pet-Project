using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public record Size
{
    public const float MIN_VALUE = 0.0f;
    public float Height { get; }
    public float Weight { get; }

    private Size(float height, float weight)
    {
        Height = height;
        Weight = weight;
    }

    public static Result<Size> Create(float height, float weight)
    {
        if (weight <= MIN_VALUE || height <= MIN_VALUE)
        {
            return Result.Failure<Size>("Breed size must be greater than zero");
        }
        
        var size = new Size(height, weight);
        return Result.Success(size);
    }
}