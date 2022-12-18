using System.Collections;
using System.Text.RegularExpressions;

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
            () =>
            {
                var mapFilesystem = MapFilesystem(terminalInput.Split(Environment.NewLine).ToArray());
                Console.WriteLine(mapFilesystem);
                return mapFilesystem;
            },
            Has.Property("Size").EqualTo(48381165));
    }

    private static Directory MapFilesystem(IEnumerable<string> commands)
    {
        var currentDirectory = new Directory("/", null);
        foreach (var cmd in commands)
        {
            var segments = cmd.Split(" ");
            switch (segments[0])
            {
                case "$" when segments[1] is "cd" && segments[2] is "/":
                    break;
                case "$" when segments[1] is "cd" && segments[2] is "..":
                    currentDirectory = currentDirectory!.ParentDirectory;
                    break;
                case "$" when segments[1] is "cd" && Regex.IsMatch(segments[2], "\\w"):
                    var dir = new Directory(segments[2], currentDirectory);
                    currentDirectory!.Content.Add(dir);
                    currentDirectory = dir;
                    break;
                case "$" when segments[1] is "ls":
                case "dir":
                    break;
                default:
                    currentDirectory!.Content.Add(new File(segments[1], int.Parse(segments[0])));
                    break;
            }
        }

        static Directory GetBaseDirectory(Directory directory)
        {
            while (true)
            {
                if (directory.Name is "/") return directory;

                directory = directory!.ParentDirectory!;
            }
        }

        return GetBaseDirectory(currentDirectory!);
    }
}