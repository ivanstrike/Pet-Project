using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Application.FileProvider;

public record UploadFileData(Stream Stream, FilePath FilePath, string BucketName);
