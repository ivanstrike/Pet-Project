using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Application.Volunteers.UpdateRequisites;
using PetProject.Application.Volunteers.UpdateSocialMedia;


namespace PetProject.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateSocialMediaHandler>();
        services.AddScoped<UpdateRequisitesHandler>();
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}