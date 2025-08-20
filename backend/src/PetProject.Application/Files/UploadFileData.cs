using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Application.FileProvider;

public record UploadFileData(Stream Stream, FileInfo FileInfo);
