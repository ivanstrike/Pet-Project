using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Volunteers;

public record SocialNetwork
{
    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }
    public string Name { get; }
    public string Link { get; }

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");
    
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsRequired("Link");
    
        return new SocialNetwork(name, link);
    }
    
}