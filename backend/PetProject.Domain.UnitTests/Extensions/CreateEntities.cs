using PetProject.Domain;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.SpeciesContext.SpeciesVO;
using PetProject.Domain.VolunteerContext;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Domain.VolunteerContext.VolunteerVO;

namespace PetProject.UnitTests.Extensions;

public static class CreateEntities
{
    public static Volunteer CreateValidVolunteer()
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var fullname = FullName.Create("Name", "Surname", null).Value;
        var email = Email.Create("email@email.com").Value;
        var description = Description.Create("Description").Value;
        var experience = Experience.Create(0).Value;
        var phoneNumber = PhoneNumber.Create("0888888888").Value;

        var volunteer =  Volunteer.Create(
            volunteerId, 
            fullname, 
            email, 
            description,
            experience, 
            phoneNumber, 
            null, 
            null);
        return volunteer.Value;
    }

    public static Pet CreateValidPet()
    {
        var petId = PetId.NewPetId();
        var name = Name.Create("Name").Value;
        var description = Description.Create("Description").Value;
        var speciesId = SpeciesId.NewSpeciesId();
        var breedId = BreedId.NewBreedId();
        var color = Color.Create("Color").Value;
        var healthInformation = HealthInformation.Create("Health Information").Value;
        var address = Address.Create("Address", "City", "Country", "5").Value;
        var size = Size.Create(5, 3).Value;
        var ownerPhone = PhoneNumber.Create("0888888888").Value;
        var isNeutered = IsNeutered.Create(true).Value;
        var birthDate = BirthDate.Create("2011-09-19").Value;
        var isVaccinated = IsVaccinated.Create(true).Value;
        var helpStatus = Status.Create("Help Status").Value;

        var pet = Pet.Create(
            petId,
            name, 
            description, 
            speciesId, 
            breedId, 
            color, 
            healthInformation,
            address, 
            size, 
            ownerPhone, 
            isNeutered, 
            birthDate, 
            isVaccinated, 
            helpStatus, 
            null,
            null).Value;
        return pet;
    }
}