using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain;
using PetProject.Domain.Shared;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Infrastructure.Options;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int MAX_DEREE_OF_PARALLELISM = 10;
    private readonly ILogger<MinioProvider> _logger;
    private readonly IMinioClient _minioClient;

    public MinioProvider(
        ILogger<MinioProvider> logger,
        IMinioClient minioClient)
    {
        _logger = logger;
        _minioClient = minioClient;
    }

/*
    public async Task<Result<string, Error>> GetFile(
        FileData uploadFileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                .WithBucket(uploadFileData.BucketName)
                .WithObject(uploadFileData.FileName)
                .WithExpiry(MinioOptions.PERSIGNED_EXPIRATION_TIME);
            var url = await _minioClient.PresignedGetObjectAsync(args);
            return url;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to get file from minio");
            return Error.Failure("file.get", "Fail to get file from minio");
        }
    }
*/

    public async Task<UnitResult<Error>> DeleteFile(
        FileInfo deleteFileInfo,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(deleteFileInfo.BucketName)
                .WithObject(deleteFileInfo.ObjectName);
            
            var objectStat = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (objectStat is null)
                return Result.Success<Error>();
            
            RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                .WithBucket(deleteFileInfo.BucketName)
                .WithObject(deleteFileInfo.ObjectName);
            await _minioClient.RemoveObjectAsync(rmArgs, cancellationToken);
            
            return Result.Success<Error>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to delete file {name} from minio bucket {bucket}", 
                deleteFileInfo.ObjectName, 
                deleteFileInfo.BucketName);
            return Error.Failure("file.delete", "Fail to delete file from minio");
        }
    }


    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEREE_OF_PARALLELISM);
        var filesList = uploadFilesData.ToList();
        try
        {
            await IfBucketNotExistCreateBucket(filesList, cancellationToken);
            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathResult = await Task.WhenAll(tasks);
            if (pathResult.Any(p => p.IsFailure))
                return pathResult.First(p => p.IsFailure).Error;

            var results = pathResult.Select(p => p.Value).ToList();
            return results;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Error while trying to upload in minio, files amount: {Amount}",
                filesList.Count);
            return Error.Failure("file.upload", "Error while trying to upload in minio");
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        UploadFileData uploadFileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(uploadFileData.BucketName)
            .WithStreamData(uploadFileData.Stream)
            .WithObjectSize(uploadFileData.Stream.Length)
            .WithObject(uploadFileData.FilePath.Value);
        try
        {
            _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            return uploadFileData.FilePath;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Error while trying to upload in minio file with path {path} in bucket {bucket}",
                uploadFileData.FilePath,
                uploadFileData.BucketName);
            return Error.Failure("file.upload", "Error while trying to upload in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketNotExistCreateBucket(
        IEnumerable<UploadFileData> filesData,
        CancellationToken cancellationToken = default)
    {
        var bucketNames = filesData.Select(x => x.BucketName).ToHashSet();
        foreach (var bucket in bucketNames)
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucket);
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistsArgs);
            if (!bucketExist)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucket);
                await _minioClient.MakeBucketAsync(makeBucketArgs);
            }
        }
    }
}