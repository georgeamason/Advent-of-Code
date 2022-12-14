// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.Write("CrateMover (9000/9001): ");
var crane = int.Parse(Console.ReadLine()!);

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
        var move = int.Parse(craneOperation[1]);
        var from = int.Parse(craneOperation[3]);
        var to = int.Parse(craneOperation[5]);
        
        switch (crane)
        {
            case 9000:
                for (var i = 0; i < move; i++) stacks[to - 1].Push(stacks[from - 1].Pop());
                break;
            case 9001:
                var popped = stacks[from - 1].Take(move).Reverse().ToList();
                for (var i = 0; i < move; i++) stacks[from - 1].Pop();
                popped.ForEach(x => stacks[to - 1].Push(x));
                break;
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