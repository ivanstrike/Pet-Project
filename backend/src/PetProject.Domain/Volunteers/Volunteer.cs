using CSharpFunctionalExtensions;

namespace PetProject.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>
{
    // for ef
    private Volunteer(VolunteerId volunteerId) : base(volunteerId) {}

    private Volunteer(
        VolunteerId volunteerId,
        FullName fullname,
        Email email,
        string description,
        float experience,
        PhoneNumber phoneNumber,
        List<SocialNetwork> socialNetworks,
        List<Requisites> requisites,
        List<Pet> pets)
        : base(volunteerId)
    {
        FullName = fullname;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
        Requisites = requisites;
        Pets = pets;
    }
    
    public VolunteerId Id { get; private set; }
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public float Experience { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public List<SocialNetwork> SocialNetworks { get; private set; } = [];
    public List<Requisites> Requisites { get; private set; } = [];
    public List<Pet> Pets { get; private set; } = [];
    public int PetsFoundHome() => Pets.Count(x => x.Status.Value == "FoundHome");
    public int PetsNeedForHome() => Pets.Count(x => x.Status.Value == "NeedForHome");
    public int PetsOnTreatment() => Pets.Count(x => x.Status.Value == "OnTreatment");
   
    public static Result Create(
        VolunteerId volunteerId,
        FullName fullname,
        string description,
        float experience,
        PhoneNumber phoneNumber,
        List<SocialNetwork> socialNetworks,
        List<Requisites> requisites,
        List<Pet> pets)
    {
        var volonteer = new Volunteer(
            volunteerId,
            fullname,
            email: default!,
            description: description,
            experience: experience,
            phoneNumber: phoneNumber,
            socialNetworks: socialNetworks,
            requisites: requisites,
            pets: pets);
        return Result.Success(volonteer);
    }
    
}