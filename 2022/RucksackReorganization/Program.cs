// See https://aka.ms/new-console-template for more information

namespace RucksackReorganization;

internal static class Program
{
    private static void Main()
    {
        using var streamReader = new StreamReader("input.txt");

        var totalPriorityError = 0;
        while (!streamReader.EndOfStream)
        {
            var contents = streamReader.ReadLine();

            var rucksack = new Rucksack(contents!);

            var compartment1 = rucksack.Compartments[0].Items;
            var compartment2 = rucksack.Compartments[1].Items;

            var error = compartment1.Intersect(compartment2)
                                    .SingleOrDefault();

            if (error is null) continue;

            totalPriorityError += error.Priority;
        }
        
        Console.WriteLine(totalPriorityError);
    }

    private sealed class Rucksack
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

        private readonly string _contents;
    }

    private sealed class Compartment
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

    private sealed class Item : IEquatable<Item>
    {
        internal Item(char item)
        {
            Type = item;
        }

        public readonly char Type;
        
        public int Priority => Alphabet.IndexOf(Type) + 1;

        public bool Equals(Item? other)
        {
            if (other is null)
                return false;

            return this.Type == other.Type;
        }

        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public override bool Equals(object? obj)
        {
            return Equals(obj as Item);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}

