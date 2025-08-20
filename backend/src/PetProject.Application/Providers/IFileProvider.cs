using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Application.Providers;

public interface IFileProvider
{
/*
    Task<Result<string, Error>> GetFile(
        FileData uploadFileData,
        CancellationToken cancellationToken = default);
*/
    Task<UnitResult<Error>> DeleteFile(
        FileInfo deleteFileInfo,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default);
}