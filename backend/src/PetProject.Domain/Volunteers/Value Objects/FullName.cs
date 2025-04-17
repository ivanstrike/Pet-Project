using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Volunteers;

public record FullName
{
    private const int MAXNAMELENGTH = 50;
    public string Name { get; }
    public string Surname { get; }
    public string? Patronymic { get; }
    

    private FullName(string name, string surname, string? patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string name, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");
    
        if (name.Length > MAXNAMELENGTH)
            return Errors.General.ValueIsInvalid("Name");
    
        if (string.IsNullOrWhiteSpace(surname))
            return Errors.General.ValueIsRequired("Surname");
    
        if (surname.Length > MAXNAMELENGTH)
            return Errors.General.ValueIsInvalid("Surname");
 
        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MAXNAMELENGTH)
            return Errors.General.ValueIsInvalid("Patronymic");
    
        return new FullName(name, surname, patronymic);
    }
    
    
}