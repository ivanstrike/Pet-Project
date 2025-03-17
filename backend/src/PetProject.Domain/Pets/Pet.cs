using CSharpFunctionalExtensions;
using PetProject.Domain.Species;

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
        string description,
        SpeciesId speciesId,
        BreedId breedId,
        Color color,
        string healthInformation,
        Address address,
        Size size,
        OwnerPhone ownerPhone,
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
    
    public string Description { get; private set; } = default!;
    
    public SpeciesId SpeciesId { get; private set; } = default!;
    
    public BreedId BreedId { get; private set; } = default!;
    
    public Color Color { get; private set; } = default!;
    
    public string HealthInformation { get; private set; } = default!;
    
    public Address Address { get; private set; } = default!;
    
    public Size Size { get; private set; }  = default!;
    
    public OwnerPhone OwnerPhone { get; private set; } = default!;
    
    public bool IsNeutered { get; private set; } = default!;

    public DateOnly BirthDate { get; private set; } = default!;
    
    public bool IsVaccinated { get; private set; } = default!;
    
    public Status Status { get; private set; } = default!;
    
    public IReadOnlyList<Requisites> Requisites => _requisites;
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public static Result Create(
        Name name,
        string description,
        SpeciesId speciesId,
        BreedId breedId,
        Color color,
        string healthInformation,
        Address address,
        Size size,
        OwnerPhone ownerPhone,
        bool isNeutered,
        DateOnly birthDate,
        bool isVaccinated,
        Status helpStatus,
        List<Requisites> requisites
        )
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure("Description is required.");
        }
        var pet = new Pet(
            PetId.NewPetId(),
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
        
        return Result.Success(pet);
    }
    
}
