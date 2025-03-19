using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Species;
using PetProject.Domain.Volunteers;

namespace PetProject.Domain;

public class Pet : Shared.Entity<PetId>
{
    private readonly List<Requisites> _requisites = [];
    //for ef
    private Pet() : base(PetId.Empty()){}
    private Pet(PetId id) : base(id){}

    public Pet(
        PetId petId,
        Name name,
        Description description,
        SpeciesId speciesId,
        BreedId breedId,
        Color color,
        HealthInformation healthInformation,
        Address address,
        Size size,
        PhoneNumber ownerPhone,
        bool isNeutered,
        DateOnly birthDate,
        bool isVaccinated,
        Status helpStatus,
        List<Requisites> requisites
        )
    :base(petId)
    {
        Name = name;
        Description = description;
        SpeciesId = speciesId;
        BreedId = breedId;
        Color = color;
        HealthInformation = healthInformation;
        Address = address;
        Size = size;
        OwnerPhone = ownerPhone;
        IsNeutered = isNeutered;
        BirthDate = birthDate;
        IsVaccinated = isVaccinated;
        Status = helpStatus;
        _requisites = requisites;
        CreatedAt = DateTime.UtcNow;
        
    }
        
    public Name Name { get; private set; } = default!;
    
    public Description Description { get; private set; } = default!;
    
    public SpeciesId SpeciesId { get; private set; } = default!;
    
    public BreedId BreedId { get; private set; } = default!;
    
    public Color Color { get; private set; } = default!;
    
    public HealthInformation HealthInformation { get; private set; } = default!;
    
    public Address Address { get; private set; } = default!;
    
    public Size Size { get; private set; }  = default!;
    
    public PhoneNumber OwnerPhone { get; private set; } = default!;
    
    public bool IsNeutered { get; private set; } = default!;

    public DateOnly BirthDate { get; private set; } = default!;
    
    public bool IsVaccinated { get; private set; } = default!;
    
    public Status Status { get; private set; } = default!;
    
    public IReadOnlyList<Requisites> Requisites => _requisites;
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public static Result<Pet, Error> Create(
        Name name,
        Description description,
        SpeciesId speciesId,
        BreedId breedId,
        Color color,
        HealthInformation healthInformation,
        Address address,
        Size size,
        PhoneNumber ownerPhone,
        bool isNeutered,
        DateOnly birthDate,
        bool isVaccinated,
        Status helpStatus,
        List<Requisites> requisites
        )
    {
        
        var petId = PetId.NewPetId();
        var pet = new Pet(
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
            requisites);

        return pet;
    }
    
}