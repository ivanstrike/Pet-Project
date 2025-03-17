using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain;
using PetProject.Domain.Shared;
using PetProject.Domain.Species;

namespace PetProject.Infrastructure.Configurations;

public class BreedConfiguration: IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));
        
        builder.ComplexProperty(b => b.Name, bb =>
        {
            bb.Property<object>(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
        });
    }
}