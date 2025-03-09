using CSharpFunctionalExtensions;

namespace PetProject.Domain.Volunteers;

public record FullName
{
    private const int MAXNAMELENGTH = 50;
    public string Name { get; }
    public string Surname { get; }
    public string Patronymic { get; }
    

    private FullName(string name, string surname, string patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }

    public static Result Create(string name, string surname, string patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name is required");
        
        if (name.Length > MAXNAMELENGTH)
            return Result.Failure($"Name cannot be more than {MAXNAMELENGTH} characters");
        
        if (string.IsNullOrWhiteSpace(surname))
            return Result.Failure("Surname is required");
        
        if (surname.Length > MAXNAMELENGTH)
            return Result.Failure($"Surname cannot be more than {MAXNAMELENGTH} characters");
        
        if (string.IsNullOrWhiteSpace(patronymic))
            return Result.Failure("Patronymic is required");
        
        if (patronymic.Length > MAXNAMELENGTH)
            return Result.Failure($"Patronymic cannot be more than {MAXNAMELENGTH} characters");
        
        
        var fullName = new FullName(name, surname, patronymic);
        return Result.Success(fullName);
    }
    
    
}