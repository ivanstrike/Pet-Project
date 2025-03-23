using CSharpFunctionalExtensions;
using PetProject.Application.DTO;
using PetProject.Domain;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;
using PetProject.Infrastructure.Repositories;

namespace PetProject.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        VolunteerDto volunteerDto,
        IEnumerable<SocialNetworkDto> socialNetworkDto,
        IEnumerable<RequisitesDto> requisitesDto,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        
        var fullName = FullName.Create(volunteerDto.Name, volunteerDto.Surname, volunteerDto.Patronymic);
        if (fullName.IsFailure)
            return fullName.Error;
        
        var email = Email.Create(volunteerDto.Email);
        if (email.IsFailure)
            return email.Error;
        
        var experience = Experience.Create(volunteerDto.Experience);
        if (experience.IsFailure)
            return experience.Error;
        
        var description = Description.Create(volunteerDto.Description);
        if (description.IsFailure)
            return description.Error;
        
        var phoneNumber = PhoneNumber.Create(volunteerDto.PhoneNumber);
        if (phoneNumber.IsFailure)
            return phoneNumber.Error;

        List<SocialNetwork> socialNetworks = [];
        foreach (var socialNetwork in socialNetworkDto)
        {
            var socialNetworkResult = SocialNetwork.Create(socialNetwork.Name, socialNetwork.Link);
            if (socialNetworkResult.IsFailure)
                return socialNetworkResult.Error;
            
            socialNetworks.Add(socialNetworkResult.Value);
        }

        List<Requisites> requisites = [];
        foreach (var requisite in requisitesDto)
        {
            var requisiteResult = Requisites.Create(requisite.Name, requisite.Description);
            if (requisiteResult.IsFailure)
                return requisiteResult.Error;
            
            requisites.Add(requisiteResult.Value);
        }
        
        var volunteer = Volunteer.Create(volunteerId, fullName.Value, email.Value, 
            description.Value, experience.Value, phoneNumber.Value, socialNetworks, requisites);
        
        await _volunteersRepository.Add(volunteer.Value, cancellationToken);

        return volunteer.Value.Id.Value;
    }
    
}