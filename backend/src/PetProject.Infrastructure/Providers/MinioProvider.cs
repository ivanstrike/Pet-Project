using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Domain.Shared;
using PetProject.Infrastructure.Options;

namespace PetProject.Infrastructure.Providers;

public class MinioProvider: IFileProvider
{
    private readonly ILogger<MinioProvider> _logger;
    private readonly IMinioClient _minioClient;

    public MinioProvider(
        ILogger<MinioProvider> logger,
        IMinioClient minioClient)
    {
        _logger = logger;
        _minioClient = minioClient;
    }

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
    
    public async Task<UnitResult<Error>> DeleteFile(
        FileData uploadFileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                .WithBucket(uploadFileData.BucketName)
                .WithObject(uploadFileData.FileName);
            await _minioClient.RemoveObjectAsync(rmArgs);
            return UnitResult.Success<Error>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to get file from minio");
            return Error.Failure("file.get", "Fail to get file from minio");
        }
    }
    public async Task<Result<string,Error>> UploadFile(
        UploadFileData uploadFileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(uploadFileData.BucketName);
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistsArgs);
            if (!bucketExist)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(uploadFileData.BucketName);
                await _minioClient.MakeBucketAsync(makeBucketArgs);
            }

            var path = Guid.NewGuid();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(uploadFileData.BucketName)
                .WithStreamData(uploadFileData.Stream)
                .WithObjectSize(uploadFileData.Stream.Length)
                .WithObject(path.ToString());

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            return result.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        
    }
}