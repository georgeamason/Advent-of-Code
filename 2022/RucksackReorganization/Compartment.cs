namespace RucksackReorganization;

internal sealed class Compartment
{
    internal Compartment(string contents)
    {
        _contents = contents ?? throw new ArgumentNullException(nameof(contents));
    }

    public IReadOnlyCollection<Item> Items
    {
        get
        {
            return _contents.Select(item => new Item(item)).ToList();
        }
    }

    private readonly string _contents;
}