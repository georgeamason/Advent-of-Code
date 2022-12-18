using System.Collections;

namespace NoSpaceLeftOnDevice;

public class Tests
{
    private static IEnumerable Example_test_data()
    {
        yield return new TestCaseData
            (System.IO.File.ReadAllText("test.txt"));
    }

    [TestCaseSource(nameof(Example_test_data))]
    public void Example(string terminalInput)
    {
        Assert.That(
            () => MapFilesystem(terminalInput.Split(Environment.NewLine).ToArray()),
            Throws.Nothing);
    }

    private static void MapFilesystem(string[] commands)
    {
        var x = commands.SkipWhile(cmd => cmd.StartsWith("$"))
                        .TakeWhile(cmd => !cmd.StartsWith("$"))
                        .ToArray();
        
        var baseDirectory = new Directory("/", x);
        baseDirectory.Directories.Single(dir => dir.Name == "/a");
    }
}