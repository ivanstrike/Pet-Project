using CSharpFunctionalExtensions;

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

    public static Result Create(string name, string link)
    {
        if  (string.IsNullOrWhiteSpace(link) || string.IsNullOrWhiteSpace(name))
            return Result.Failure("Social network link is empty");
        
        var socialNetwork = new SocialNetwork(name, link);
        return Result.Success(socialNetwork);
    }
    
}