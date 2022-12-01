// See https://aka.ms/new-console-template for more information

using var streamReader = new StreamReader("input.txt");

var calorieCounter = new List<int>();

while (!streamReader.EndOfStream)
{
    var totalCalories = 0;
    while (true)
    {
        var calories = streamReader.ReadLine();
        if (string.IsNullOrWhiteSpace(calories))
            break;

        totalCalories += int.Parse(calories);
    }

    calorieCounter.Add(totalCalories); 
}

var ordered = calorieCounter.OrderByDescending(i => i).ToArray();
var max = ordered[0];
var elf = calorieCounter.IndexOf(max);
Console.WriteLine($"Elf {elf} is carrying {max} calories! Talk to him for snacks!");

var topThree = ordered.Take(3).Sum();

int IndexOf(int index)
{
    return calorieCounter.IndexOf(ordered[index]);
}

Console.WriteLine($"The top three Elves are {IndexOf(0)}, " +
                  $"{IndexOf(1)}, {IndexOf(2)} " +
                  $"carrying a total of {topThree} calories");