using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IFileProvider  _fileProvider;

    public AddPetHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Handle(
        UploadFileData uploadFileData,
        CancellationToken cancellationToken = default)
    {
        var result = await _fileProvider.UploadFile(uploadFileData, cancellationToken);
        return result;
    }
}