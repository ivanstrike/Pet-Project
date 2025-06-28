using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.SpeciesHandlers.CreateSpecies;
using PetProject.Application.Volunteers.AddPet;
using PetProject.Application.Volunteers.AddPetFiles;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.DeletePetFiles;
using PetProject.Application.Volunteers.DeleteVolunteer;
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
        services.AddScoped<DeleteHardVolunteerHandler>();
        services.AddScoped<DeleteSoftVolunteerHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<AddPetFilesHandler>();
        services.AddScoped<DeletePetFilesHandler>();
        services.AddScoped<CreateSpeciesHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}