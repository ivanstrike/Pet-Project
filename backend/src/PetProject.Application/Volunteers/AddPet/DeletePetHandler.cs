using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers.AddPet;

public class DeletePetHandler
{
    private readonly IFileProvider _fileProvider;

    public DeletePetHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    
    public async Task<UnitResult<Error>> Handle(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        var result = await _fileProvider.DeleteFile(fileData, cancellationToken);
        return result;
    }
}