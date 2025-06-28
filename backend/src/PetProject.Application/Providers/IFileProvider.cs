using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Application.Providers;

public interface IFileProvider
{
/*
    Task<Result<string, Error>> GetFile(
        FileData uploadFileData,
        CancellationToken cancellationToken = default);
*/
    Task<UnitResult<Error>> DeleteFiles(
        IEnumerable<FileData> deleteFilesData,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default);
}