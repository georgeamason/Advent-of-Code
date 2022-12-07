namespace RucksackReorganization;

internal sealed class Rucksack
{
    internal Rucksack(string contents)
    {
        _contents = contents ?? throw new ArgumentNullException(nameof(contents));
    }

    private int Length => _contents.Length;

    public IReadOnlyList<Compartment> Compartments
    {
        get
        {
            return new[]
            {
                new Compartment(_contents[..(Length / 2)]),
                new Compartment(_contents[(Length / 2)..])
            };
        }
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