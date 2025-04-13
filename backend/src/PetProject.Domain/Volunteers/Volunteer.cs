using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;

namespace PetProject.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private IReadOnlyList<SocialNetwork> _socialNetworks = [];
    private IReadOnlyList<Requisites> _requisites = [];
    private readonly List<Pet> _pets = [];
    
    private bool _isDeleted = false;
    
    // for ef
    private Volunteer() : base(VolunteerId.Empty()) {}
    private Volunteer(VolunteerId volunteerId) : base(volunteerId) {}

    public Volunteer(
        VolunteerId volunteerId,
        FullName fullname,
        Email email,
        Description description,
        Experience experience,
        PhoneNumber phoneNumber,
        List<SocialNetwork> socialNetworks,
        List<Requisites> requisites)
        : base(volunteerId)
    {
        FullName = fullname;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
        _socialNetworks = socialNetworks;
        _requisites = requisites;
    }
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Experience Experience { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisites> Requisites => _requisites;
    public IReadOnlyList<Pet> Pets => _pets;
    public int PetsFoundHome() => Pets.Count(x => x.Status.Value == "FoundHome");
    public int PetsNeedForHome() => Pets.Count(x => x.Status.Value == "NeedForHome");
    public int PetsOnTreatment() => Pets.Count(x => x.Status.Value == "OnTreatment");
   
    public void Delete()
    {
        if (!_isDeleted)
        {
            _isDeleted = true;
            DeletePets();
        }
    }
    public void DeletePets()
    {
        foreach (var pet in _pets)
        {
            pet.Delete();
        }
    }
    public void RestorePets()
    {
        foreach (var pet in _pets)
        {
            pet.Restore();
        }
    }
    public void Restore()
    {
        if (_isDeleted)
        {
            _isDeleted = false;
            RestorePets();
        }
    }
    
    public void UpdateMainInfo(
        FullName fullname,
        Email email,
        Description description,
        Experience experience,
        PhoneNumber phoneNumber)
    {
        FullName = fullname;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
    }
    public static Result<Volunteer, Error> Create(
        VolunteerId volunteerId,
        FullName fullname,
        Email email,
        Description description,
        Experience experience,
        PhoneNumber phoneNumber,
        List<SocialNetwork> socialNetworks,
        List<Requisites> requisites)
    {
       var volonteer = new Volunteer(
            volunteerId,
            fullname,
            email,
            description,
            experience,
            phoneNumber,
            socialNetworks,
            requisites);

        return volonteer;
    }


    public void UpdateSocialMedia(List<SocialNetwork> socialNetworks)
    {
        _socialNetworks = socialNetworks;
    }
    
    public void UpdateRequisites(List<Requisites> requisites)
    {
        _requisites = requisites;
    }
}