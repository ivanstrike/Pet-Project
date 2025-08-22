using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain.Shared;
using PetProject.Domain.SpeciesContext.SpeciesVO;
using PetProject.Domain.VolunteerContext;
using PetProject.Domain.VolunteerContext.PetVO;
using PetProject.Infrastructure.Extensions;

namespace PetProject.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value,
                value => PetId.Create(value));

        builder.ComplexProperty(p => p.Name, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
        });

        builder.ComplexProperty(p => p.Description, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("description");
        });

        builder.Property(p => p.SpeciesId)
            .HasConversion(id => id.Value,
                value => SpeciesId.Create(value));

        builder.Property(p => p.BreedId)
            .HasConversion(id => id.Value,
                value => BreedId.Create(value));

        builder.ComplexProperty(p => p.Color, cb =>
        {
            cb.Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("color");
        });

        builder.ComplexProperty(p => p.HealthInformation, cb =>
        {
            cb.Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("health_information");
        });

        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("country");

            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("city");

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("street");

            ab.Property(a => a.HouseNumber)
                .IsRequired()
                .HasColumnName("house_number");
        });

        builder.ComplexProperty(p => p.Size, sb =>
        {
            sb.Property(s => s.Height)
                .IsRequired()
                .HasColumnName("height");

            sb.Property(s => s.Weight)
                .IsRequired()
                .HasColumnName("weight");
        });

        builder.ComplexProperty(p => p.OwnerPhone, ob =>
        {
            ob.Property(o => o.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_NUMBER_LENGTH)
                .HasColumnName("owner_phone");
        });

        builder.ComplexProperty(p => p.IsNeutered, ob =>
        {
            ob.Property(o => o.Value)
                .IsRequired()
                .HasColumnName("is_neutered");
        });

        builder.ComplexProperty(p => p.BirthDate, ob =>
        {
            ob.Property(o => o.Value)
                .IsRequired()
                .HasColumnName("birth_date");
        });

        builder.ComplexProperty(p => p.IsVaccinated, ob =>
        {
            ob.Property(o => o.Value)
                .IsRequired()
                .HasColumnName("is_vaccinated");
        });

        builder.ComplexProperty(p => p.Status, sb =>
        {
            sb.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("status");
        });

        builder.Property(p => p.Requisites)
            .JsonValueObjectCollectionConversion()
            .HasColumnName("requisites");

        builder.Property(p => p.Files)
            .JsonValueObjectCollectionConversion()
            .HasColumnName("files");

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.ComplexProperty(p => p.SerialNumber, sb =>
        {
            sb.Property(s => s.Value)
                .IsRequired(true)
                .HasColumnName("serial_number");
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}