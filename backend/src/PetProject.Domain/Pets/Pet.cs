using CSharpFunctionalExtensions;

namespace PetProject.Domain;

public class Pet : Shared.Entity<PetId>
{
    //for ef
    private Pet(PetId id) : base(id){}

    public Pet(
        PetId petId,
        string name,
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
        Requisites requisites
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
        Requisites = requisites;
        CreatedAt = DateTime.UtcNow;
        
    }
        
    public string Name { get; private set; } = default!;
    
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
    
    public Requisites Requisites { get; private set; } = default!;
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public static Result Create(
        string name,
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
        Requisites requisites
        )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure("Name is required.");
        }

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
