using PetProject.Application.Files;
using PetProject.Application.MessageQueues;
using PetProject.Application.Providers;
using FileInfo = PetProject.Application.Files.FileInfo;

namespace PetProject.Infrastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IFileProvider _fileProvider;

    public FilesCleanerService(IFileProvider fileProvider,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _fileProvider = fileProvider;
        _messageQueue = messageQueue;
    }

    public async Task Process(CancellationToken stoppingToken)
    {
        var files = await _messageQueue.ReadAsync(stoppingToken);

        foreach (var fileInfo in files)
        {
            await _fileProvider.DeleteFile(fileInfo, stoppingToken);
        }
    }
}