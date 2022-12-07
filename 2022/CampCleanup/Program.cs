// See https://aka.ms/new-console-template for more information

using var sr = new StreamReader("input.txt");

var count = 0;
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

    bool Engulfs(IReadOnlyList<int> datum, IReadOnlyList<int> spec)
    {
        return (datum[0] <= spec[0] && datum[^1] >= spec[^1])
               || (spec[0] <= datum[0] && spec[^1] >= datum[^1]);
    }

    var x = ElfRange(0);
    var y = ElfRange(1);
    if (Engulfs(x, y))
        count++;
}

Console.WriteLine(count);