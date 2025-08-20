using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetProject.Application.Database;
using PetProject.Application.MessageQueues;
using PetProject.Application.Providers;
using PetProject.Infrastructure.MessageQueues;
using PetProject.Infrastructure.Options;
using PetProject.Infrastructure.Providers;
using PetProject.Infrastructure.Repositories;
using FileInfo = PetProject.Application.FileProvider.FileInfo;

namespace PetProject.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddMinio(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        
        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
        services.AddMinio(options =>
        {
            var miniopOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                                ?? throw new ApplicationException("Minio configuration not found");
            options.WithEndpoint(miniopOptions.Endpoint);
            options.WithCredentials(miniopOptions.AccessKey,  miniopOptions.SecretKey);
            options.WithSSL(miniopOptions.WithSSL);
        });
        
        services.AddScoped<IFileProvider, MinioProvider>();
        
        return services;
    }
}