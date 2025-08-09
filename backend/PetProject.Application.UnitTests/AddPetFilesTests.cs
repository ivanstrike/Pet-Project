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
using PetProject.Application.Providers;
using PetProject.Application.Volunteers.AddPetFiles;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.UnitTests.Extensions;

namespace PetProject.Application.UnitTests;

public class AddPetFilesTests
{
    [Fact]
    public async Task Handle_Should_Upload_Files_To_Pet()
    {
        //arrange
        var logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger<AddPetFilesHandler>();
        
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
        
        var fileProviderMock = new Mock<IFileProvider>();
        fileProviderMock
            .Setup(v => v.UploadFiles(It.IsAny<IEnumerable<UploadFileData>>(), cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));
            
        var volunteersRepositoryMock = new Mock<IVolunteersRepository>();
        volunteersRepositoryMock.Setup(v => v.GetById(voluteer.Id, cancellationToken))
            .ReturnsAsync(voluteer);

        var validatorMock = new Mock<IValidator<AddPetFilesCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());
        
        var transactionMock = new Mock<IDbTransaction>();
        transactionMock.Setup(t => t.Commit()).Verifiable();
        transactionMock.Setup(t => t.Rollback()).Verifiable();

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);
        unitOfWorkMock.Setup(u => u.BeginTransaction(cancellationToken))
            .ReturnsAsync(transactionMock.Object);
        
        var handler = new AddPetFilesHandler(
            fileProviderMock.Object,
            volunteersRepositoryMock.Object,
            validatorMock.Object,
            unitOfWorkMock.Object,
            logger
            );

        //act
        var result = await handler.Handle(command, cancellationToken);

        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(pet.Id.Value);
    }
}