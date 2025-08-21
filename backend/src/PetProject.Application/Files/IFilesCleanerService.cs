namespace PetProject.Application.Files;

public interface IFilesCleanerService
{
    Task Process(CancellationToken stoppingToken);
}