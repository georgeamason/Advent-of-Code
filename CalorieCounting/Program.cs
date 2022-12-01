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

var max = calorieCounter.Max();
var elf = calorieCounter.IndexOf(max);
Console.WriteLine($"Elf {elf} is carrying {max} calories! Talk to him for snacks!");

var topThree = calorieCounter.OrderByDescending(i => i)
                             .Take(3)
                             .Sum();

Console.WriteLine($"The top three Elves are carrying a total of {topThree} calories");