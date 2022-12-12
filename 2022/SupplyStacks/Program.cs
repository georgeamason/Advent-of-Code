// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Text.RegularExpressions;

var stacks = new List<Stack<char>>();
using var sr = new StreamReader("input.txt");

while (!sr.EndOfStream)
{
    var line = sr.ReadLine();

    if (!string.IsNullOrEmpty(line) && !line.StartsWith(" "))
    {
        var crates = line.Chunk(4).Select(crate => new String(crate));

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
    else
    {
        var x = 1;
        // process to movement with crane        
    }
    
}