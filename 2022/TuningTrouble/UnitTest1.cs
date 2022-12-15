namespace TuningTrouble;

public class Tests
{
    // The start of a packet is indicated by a sequence of four characters that are all different.

    [Test]
    public void Example1()
    {
        Assert.That(() => ComputeMarker("mjqjpqmgbljsphdztnvjfqwrcgsmlb"), Is.EqualTo(7));
    }
    
    [Test]
    public void Example2()
    {
        Assert.That(() => ComputeMarker("bvwbjplbgvbhsrlpgdmjqwftvncz"), Is.EqualTo(5));
    }

    [Test]
    public void Part1()
    {
        var dataStream = File.ReadAllText("input.txt");
        
        Assert.That(() =>
        {
            var marker = ComputeMarker(dataStream);
            Console.WriteLine(marker);
            return marker;
        }, Is.InstanceOf<int>());
    }

    private static int ComputeMarker(string dataStream)
    {
        for (var i = 0; i < dataStream.Length; i++)
        {
            if (dataStream.Length < i + 4)
                throw new IndexOutOfRangeException("No marker found!");
                
            if (dataStream[new Range(i, i + 4)].Distinct().Count() is 4)
                return i + 4;
        }

        throw new ArgumentException();
    }
}