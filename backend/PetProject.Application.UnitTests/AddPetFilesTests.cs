using System.Data;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetProject.Application.Database;
using PetProject.Application.DTO;
using PetProject.Application.FileProvider;
using PetProject.Application.MessageQueues;
using PetProject.Application.Providers;
using PetProject.Application.Volunteers.AddPetFiles;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.UnitTests.Extensions;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Application.UnitTests;

public class AddPetFilesTests
{
    private readonly Mock<IFileProvider> _fileProviderMock = new();
    private readonly Mock<IVolunteersRepository> _volunteersRepositoryMock = new();
    private readonly Mock<IDbTransaction> _transactionMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ILogger<AddPetFilesHandler>> _loggerMock = new();
    private readonly Mock<IValidator<AddPetFilesCommand>> _validatorMock = new();
    private readonly Mock<IMessageQueue<IEnumerable<FileInfo>>> _messageQueueMock = new();
    
    [Fact]
    public async Task Handle_Should_Upload_Files_To_Pet()
    {
        //arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var voluteer = CreateEntities.CreateValidVolunteer();

        var pet = CreateEntities.CreateValidPet();

        voluteer.AddPet(pet);

        var stream = new MemoryStream();
        var fileName = "test.jpg";

        var uploadFileDto = new UploadFileDto(stream, fileName);
        List<UploadFileDto> files = [uploadFileDto, uploadFileDto];

        var command = new AddPetFilesCommand(voluteer.Id.Value, pet.Id.Value, files);

        List<FilePath> filePaths =
        [
            FilePath.Create(fileName).Value,
            FilePath.Create(fileName).Value
        ];
        
        _fileProviderMock
            .Setup(v => v.UploadFiles(It.IsAny<IEnumerable<UploadFileData>>(), cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));
            
        _volunteersRepositoryMock.Setup(v => v.GetById(voluteer.Id, cancellationToken))
            .ReturnsAsync(voluteer);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());
        
        _transactionMock.Setup(t => t.Commit()).Verifiable();
        _transactionMock.Setup(t => t.Rollback()).Verifiable();

        _unitOfWorkMock.Setup(u => u.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.BeginTransaction(cancellationToken))
            .ReturnsAsync(_transactionMock.Object);
        
        var handler = new AddPetFilesHandler(
            _fileProviderMock.Object,
            _volunteersRepositoryMock.Object,
            _validatorMock.Object,
            _unitOfWorkMock.Object,
            _messageQueueMock.Object,
            _loggerMock.Object
            );

        //act
        var result = await handler.Handle(command, cancellationToken);

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(pet.Id.Value);
    }
}