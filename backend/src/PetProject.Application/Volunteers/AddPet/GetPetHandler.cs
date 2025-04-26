using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers.AddPet;

public class GetPetHandler
{
    private readonly IFileProvider _fileProvider;

    public GetPetHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    
    public async Task<Result<string, Error>> Handle(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        var result = await _fileProvider.GetFile(fileData, cancellationToken);
        return result;
    }
}