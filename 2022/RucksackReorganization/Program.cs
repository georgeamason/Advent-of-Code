// See https://aka.ms/new-console-template for more information

namespace RucksackReorganization;

internal static class Program
{
    private static void Main()
    {
        Part1();
        Part2();
    }

    private static void Part1()
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

    private static void Part2()
    {
        var groups = File.ReadAllLines("input.txt")
                         .Chunk(3)
                         .ToList();

        var totalPriorityError = 0;
        foreach (var group in groups)
        {
            var pack1 = new Rucksack(group[0]).Items;
            var pack2 = new Rucksack(group[1]).Items;
            var pack3 = new Rucksack(group[2]).Items;

            var error = pack1.Intersect(pack2)
                          .Intersect(pack3)
                          .Single();

            totalPriorityError += error.Priority;
        }

        Console.WriteLine(totalPriorityError);
    }
}

