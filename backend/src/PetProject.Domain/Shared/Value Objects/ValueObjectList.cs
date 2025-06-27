using System.Collections;

namespace PetProject.Domain.Shared.Value_Objects;

public class ValueObjectList<T> : IReadOnlyList<T>
{
    public IReadOnlyList<T> Values { get; } = null!;
    public int Count => Values.Count;
    
    public T this[int index] => Values[index];

    private ValueObjectList()
    {
        Values = new List<T>().AsReadOnly();
    }
    
    public ValueObjectList(IEnumerable<T> list) => 
        Values = new List<T>(list).AsReadOnly();

    public static ValueObjectList<T> Empty() => new();
    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();

    public static implicit operator ValueObjectList<T>(List<T> list) => new(list);
    
    public static implicit operator List<T>(ValueObjectList<T> list) => list.Values.ToList();
    
    
}