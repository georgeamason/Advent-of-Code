namespace TuningTrouble;

public class Tests
{
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 4,  7)]
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14, 19)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 4, 5)]
    public void Compute_marker_calculates_marker_correctly
        (string input, int distinctCharacters, int result)
    {
        Assert.That(() => ComputeMarker(input, distinctCharacters), Is.EqualTo(result));
    }

    [TestCase(4)]
    [TestCase(14)]
    public void Program(int distinctCharacters)
    {
        var dataStream = File.ReadAllText("input.txt");
        
        Assert.That(() => ComputeMarker(dataStream, distinctCharacters), Is.InstanceOf<int>());
    }

    private static int ComputeMarker(string dataStream, int distinctCharacters = 4)
    {
        for (var i = 0; i < dataStream.Length; i++)
        {
            var index = i + distinctCharacters;

            if (dataStream.Length < index)
                throw new IndexOutOfRangeException("No marker found!");

            if (dataStream[new Range(i, index)].Distinct().Count() == distinctCharacters)
            {
                Console.WriteLine(index);
                return index;
            }
        }

        throw new ArgumentException("No marker found!");
    }
}