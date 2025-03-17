using CSharpFunctionalExtensions;

namespace PetProject.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<SocialNetwork> _socialNetworks = [];
    private readonly List<Requisites> _requisites = [];
    private readonly List<Pet> _pets = [];
    // for ef
    private Volunteer() : base(VolunteerId.Empty()) {}
    private Volunteer(VolunteerId volunteerId) : base(volunteerId) {}

    public Volunteer(
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
        _socialNetworks = socialNetworks;
        _requisites = requisites;
        _pets = pets;
    }
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public float Experience { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisites> Requisites => _requisites;
    public IReadOnlyList<Pet> Pets => _pets;
    public int PetsFoundHome() => Pets.Count(x => x.Status.Value == "FoundHome");
    public int PetsNeedForHome() => Pets.Count(x => x.Status.Value == "NeedForHome");
    public int PetsOnTreatment() => Pets.Count(x => x.Status.Value == "OnTreatment");
   
    public static Result Create(
        FullName fullname,
        Email email,
        string description,
        float experience,
        PhoneNumber phoneNumber,
        List<SocialNetwork> socialNetworks,
        List<Requisites> requisites,
        List<Pet> pets)
    {
        var volunteerId = VolunteerId.NewPetId();
        var volonteer = new Volunteer(
            volunteerId,
            fullname,
            email,
            description,
            experience,
            phoneNumber,
            socialNetworks,
            requisites,
            pets: pets);
        return Result.Success(volonteer);
    }
    
}