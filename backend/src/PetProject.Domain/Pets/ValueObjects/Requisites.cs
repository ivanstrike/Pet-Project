using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public record Requisites
{
    public string Name { get;}
    public string Description { get;}

    private Requisites(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure("Required fields are missing.");
        }
        
        var requisites = new Requisites(name, description);
        return Result.Success(requisites);
    }
}