namespace RucksackReorganization;

internal sealed class Item : IEquatable<Item>
{
    internal Item(char type)
    {
        _type = type;
    }

    private readonly char _type;
        
    public int Priority => Alphabet.IndexOf(_type) + 1;

    public bool Equals(Item? other)
    {
        if (other is null)
            return false;

        return _type == other._type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Item);
    }

    public override int GetHashCode()
    {
        return _type.GetHashCode();
    }

    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
}