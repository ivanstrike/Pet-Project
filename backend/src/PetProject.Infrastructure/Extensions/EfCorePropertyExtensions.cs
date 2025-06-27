using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain.Shared.Value_Objects;

namespace PetProject.Infrastructure.Extensions;

public static class EfCorePropertyExtensions
{
    public static PropertyBuilder<TValueObject> JsonValueObjectConversion<TValueObject>(
        this PropertyBuilder<TValueObject> builder)
    {
        return builder.HasConversion(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<TValueObject>(v, JsonSerializerOptions.Default)!);
    }
    
    public static PropertyBuilder<IReadOnlyList<TValueObject>> JsonValueObjectCollectionConversion<TValueObject>(
        this PropertyBuilder<IReadOnlyList<TValueObject>> builder)
    {
        return builder.HasConversion(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<IReadOnlyList<TValueObject>>(v, JsonSerializerOptions.Default)!,
            new ValueComparer<IReadOnlyList<TValueObject>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => c.ToList()));
    }
    
    public static PropertyBuilder<ValueObjectList<TValueObject>> JsonValueObjectCollectionConversion<TValueObject>(
        this PropertyBuilder<ValueObjectList<TValueObject>> builder)
    {
        return builder.HasConversion(
            v => JsonSerializer.Serialize(v.Values, JsonSerializerOptions.Default),
            v => new ValueObjectList<TValueObject>(JsonSerializer.Deserialize<List<TValueObject>>(v, JsonSerializerOptions.Default)!),
            new ValueComparer<ValueObjectList<TValueObject>>(
                (c1, c2) => c1!.Values.SequenceEqual(c2!.Values),
                c => c.Values.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => new ValueObjectList<TValueObject>(c.Values.ToList()))
        );
    }

}