using CSharpFunctionalExtensions;

namespace PetProject.Domain.Volunteers;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure("First and last name are required");
        }
        var fullName = new FullName(firstName, lastName);
        return Result.Success(firstName);
    }
    
    
}