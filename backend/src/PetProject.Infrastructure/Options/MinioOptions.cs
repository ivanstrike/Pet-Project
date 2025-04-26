namespace PetProject.Infrastructure.Options;

public class MinioOptions
{
    public const string MINIO = "Minio";
    public const int PERSIGNED_EXPIRATION_TIME = 60 * 60 * 24;
    public string Endpoint { get; init; }
    public string AccessKey { get; init; }
    public string SecretKey { get; init; }
    public bool WithSSL{ get; init; }
}