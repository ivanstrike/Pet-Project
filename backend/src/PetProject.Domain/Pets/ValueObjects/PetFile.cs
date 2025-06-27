using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Pets.ValueObjects;

public record PetFile
{
    public PetFile(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    public FilePath PathToStorage { get; }
}