// See https://aka.ms/new-console-template for more information

using var sr = new StreamReader("input.txt");

var engulfs = 0;
var overlaps = 0;
while (!sr.EndOfStream)
{
    var line = sr.ReadLine();
    var pairs = line!.Split(',');

    IReadOnlyList<int> ElfRange(int elfIndex)
    {
        var indexes = pairs![elfIndex].Split('-');
        var start = int.Parse(indexes[0]);
        var end = int.Parse(indexes[1]);
        
        return Enumerable.Range(start, end - start + 1).ToArray();
    }
    
    var x = ElfRange(0);
    var y = ElfRange(1);
    
    bool Engulfs()
    {
        return (x[0] <= y[0] && x[^1] >= y[^1])
               || (y[0] <= x[0] && y[^1] >= x[^1]);
    }

    bool Overlaps()
    {
        return x!.Intersect(y!).Any();
    }

    if (Engulfs()) 
        engulfs++;

    if (Overlaps())
        overlaps++;
}

Console.WriteLine(engulfs);
Console.WriteLine(overlaps);