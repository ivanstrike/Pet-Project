using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Application.FileProvider;

public record FileInfo(FilePath FilePath, string BucketName);