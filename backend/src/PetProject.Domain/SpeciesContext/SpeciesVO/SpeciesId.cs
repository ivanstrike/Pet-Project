﻿namespace PetProject.Domain.SpeciesContext.SpeciesVO;

public record SpeciesId
{
    private SpeciesId(Guid value) => Value = value;
    public Guid Value { get; }
    
    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
    
    public static SpeciesId Empty()  => new(Guid.Empty);
    
    public static SpeciesId Create(Guid id) => new (id);
    public static implicit operator Guid(SpeciesId id) => id.Value;
}