using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Domain.Shared;

namespace PetProject.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> GetFile(
        FileData uploadFileData,
        CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFile(
        FileData uploadFileData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> UploadFile(
        UploadFileData uploadFileData,
        CancellationToken cancellationToken = default);
}