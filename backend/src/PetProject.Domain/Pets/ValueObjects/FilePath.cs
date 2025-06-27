using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.Pets.ValueObjects;


public record FilePath
{
    public string Value { get; }

    [JsonConstructor]
    public FilePath(string Value)
    {
        this.Value = Value;
    }

    public static Result<FilePath, Error> Create(string path) => new FilePath(path);
    public static Result<FilePath, Error> Create(Guid path, string extension) => new FilePath(path + extension);
}
