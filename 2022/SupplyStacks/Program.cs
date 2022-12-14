// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

using var sr = new StreamReader("input.txt");
var stacks = new List<Stack<char>>();

// Build the stack
string? line;
while ((line = sr.ReadLine()) != null && line.Contains('['))
{
    var crates = line.Chunk(4).Select(crate => new string(crate));

    var i = 0;
    foreach (var crate in crates)
    {
        if (stacks.Count < i + 1)
            stacks.Add(new Stack<char>());

        if (char.TryParse(Regex.Match(crate!, "\\w").Value, out var item))
            stacks[i].Push(item);

        i++;
    }
}

// reverse the stacks
stacks = stacks.Select(s => new Stack<char>(s)).ToList();

// Process the crane movement
while (!sr.EndOfStream)
{
    line = sr.ReadLine();

    if (line!.StartsWith("move"))
    {
        var craneOperation = line!.Split(" ");

        for (var i = 0; i < int.Parse(craneOperation[1]); i++)
        {
            var popped = stacks[int.Parse(craneOperation[3]) - 1].Pop();
            stacks[int.Parse(craneOperation[5]) - 1].Push(popped);
        }
    }
}

var result = string.Empty;
foreach (var stack in stacks)
{
    if (stack.TryPeek(out var top))
    {
        result += top;
    }
}

Console.WriteLine(result);