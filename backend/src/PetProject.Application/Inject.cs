using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.Volunteers.CreateVolunteer;

namespace PetProject.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        
        return services;
    }
}