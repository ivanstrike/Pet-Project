using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

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

    public static Result<Requisites, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            return Errors.General.ValueIsRequired("Requisites");
        }
        
        return new Requisites(name, description);
    }
}