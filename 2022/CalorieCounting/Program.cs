// See https://aka.ms/new-console-template for more information

using var streamReader = new StreamReader("input.txt");

var unorderedElfCalories = new List<int>();

while (!streamReader.EndOfStream)
{
    var elfCalories = 0;
    while (true)
    {
        var calories = streamReader.ReadLine();
        if (string.IsNullOrWhiteSpace(calories))
            break;

        elfCalories += int.Parse(calories);
    }

    unorderedElfCalories.Add(elfCalories); 
}

var orderedElfCalories = unorderedElfCalories.OrderByDescending(i => i).ToArray();

int ElfFinder(int calories)
{
    return unorderedElfCalories.IndexOf(calories);
}


Console.WriteLine($"|{"Elf",5}|{"Calories",10}|{"Cumulative", 15}|");
Console.WriteLine($"|-----|----------|---------------|");

var cumulative = 0;
foreach (var elfCalories in orderedElfCalories)
{
    cumulative += elfCalories;
    Console.WriteLine($"|{ElfFinder(elfCalories),5}|{elfCalories,10}|{cumulative, 15}|");
}