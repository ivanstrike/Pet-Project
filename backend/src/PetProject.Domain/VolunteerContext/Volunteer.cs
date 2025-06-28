using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Domain.VolunteerContext.VolunteerVO;

namespace PetProject.Domain.VolunteerContext;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private IReadOnlyList<SocialNetwork> _socialNetworks = [];
    private IReadOnlyList<Requisites> _requisites = [];
    private readonly List<Pet> _pets = [];

    private bool _isDeleted = false;

    // for ef
    private Volunteer() : base(VolunteerId.Empty())
    {
    }

    private Volunteer(VolunteerId volunteerId) : base(volunteerId)
    {
    }

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

    public UnitResult<Error> AddPet(Pet pet)
    {
        var serialNumberResult = SerialNumber.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return serialNumberResult.Error;
        pet.SetSerialNumber(serialNumberResult.Value);

        _pets.Add(pet);
        return Result.Success<Error>();
    }

    public UnitResult<Error> MovePet(SerialNumber fromSerialNumber, SerialNumber toSerialNumber)
    {
        if (fromSerialNumber == toSerialNumber)
            return Result.Success<Error>();

        var movedPet = _pets.FirstOrDefault(x => x.SerialNumber == fromSerialNumber);
        if (movedPet == null)
            return Errors.Volunteer.PetNotFound(fromSerialNumber);
        
        if (toSerialNumber.Value < 1 || toSerialNumber.Value > _pets.Count + 1)
            return Errors.General.ValueIsInvalid("SerialNumber");
        
        var sortedPets = _pets.OrderBy(x => x.SerialNumber.Value).ToList();

        sortedPets.Remove(movedPet);

        sortedPets.Insert(toSerialNumber.Value - 1, movedPet);
        
        for (int i = 0; i < sortedPets.Count; i++)
        {
            sortedPets[i].SetSerialNumber(SerialNumber.Create(i + 1).Value);
        }

        return Result.Success<Error>();
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId);
        if (pet == null)
            return Errors.General.NotFound(petId.Value);
        return pet;
    }
}