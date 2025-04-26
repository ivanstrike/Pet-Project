namespace PetProject.Application.FileProvider;

public record UploadFileData(Stream Stream, string BucketName, string FileName);