using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.SpeciesContext.SpeciesVO;
using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Domain.VolunteerContext;

public class Pet : Shared.Entity<PetId>
{
    private readonly List<Requisites> _requisites = [];
    
    private IReadOnlyList<PetFile> _files = [];
    
    private bool _isDeleted = false;
    

    //for ef
    private Pet() : base(PetId.Empty())
    {
    }

    private Pet(PetId id) : base(id)
    {
    }

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
        IsNeutered isNeutered,
        BirthDate birthDate,
        IsVaccinated isVaccinated,
        Status helpStatus,
        List<Requisites> requisites
    )
        : base(petId)
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
        CreatedAt = DateTime.UtcNow;;
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public SpeciesId SpeciesId { get; private set; } = default!;

    public BreedId BreedId { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public HealthInformation HealthInformation { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public Size Size { get; private set; } = default!;

    public PhoneNumber OwnerPhone { get; private set; } = default!;

    public IsNeutered IsNeutered { get; private set; } = default!;

    public BirthDate BirthDate { get; private set; } = default!;

    public IsVaccinated IsVaccinated { get; private set; } = default!;
    
    public Status Status { get; private set; } = default!;
    
    public IReadOnlyList<Requisites> Requisites => _requisites;

    public IReadOnlyList<PetFile> Files => _files;
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    public SerialNumber SerialNumber { get; private set; }

    public void UpdateFiles(IEnumerable<PetFile> files)
    {
        var newFiles = _files.ToList();
        newFiles.AddRange(files);
        _files = newFiles;
    }
    
    public void DeleteFiles(IEnumerable<PetFile> files)
    {
        var newFiles = _files.ToList();
        foreach (var file in files)
        {
            newFiles.Remove(file);
        }
        _files = newFiles;
    }
    public void SetSerialNumber(SerialNumber number) => SerialNumber = number;

    public void Delete()
    {
        if (!_isDeleted)
            _isDeleted = true;
    }

    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
    }
    
    public static Result<Pet, Error> Create(
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
        IsNeutered isNeutered,
        BirthDate birthDate,
        IsVaccinated isVaccinated,
        Status helpStatus,
        List<Requisites> requisites
    )
    {
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