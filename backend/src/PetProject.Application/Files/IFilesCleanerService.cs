namespace PetProject.Infrastructure.BackgroundServices;

public interface IFilesCleanerService
{
    Task Process(CancellationToken stoppingToken);
}