using PetProject.Domain;
using PetProject.Domain.Pets.ValueObjects;

namespace PetProject.Application.FileProvider;

public record UploadFileData(Stream Stream, FilePath FilePath, string BucketName);
