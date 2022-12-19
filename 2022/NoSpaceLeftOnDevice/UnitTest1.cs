using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NoSpaceLeftOnDevice;

public class Tests
{
    private static IEnumerable Base_directory_expected_size_test_data()
    {
        yield return new TestCaseData
            (System.IO.File.ReadAllText("test.txt"), 48381165);
        
        yield return new TestCaseData
            (System.IO.File.ReadAllText("input.txt"), 41412830);
    }

    [Test, TestCaseSource(nameof(Base_directory_expected_size_test_data))]
    public void Base_directory_is_expected_total_size(string terminalInput, int size)
    {
        Assert.That(
            () => MapFilesystem(terminalInput.Split(Environment.NewLine).ToArray()).Item1,
            Has.Property("Size").EqualTo(size));
    }

    private static IEnumerable Directories_of_maximum_size_100000_test_data()
    {
        yield return new TestCaseData
            (System.IO.File.ReadAllText("test.txt"), 95437);
        
        yield return new TestCaseData
            (System.IO.File.ReadAllText("input.txt"), 1749646);
    }

    [Test, TestCaseSource(nameof(Directories_of_maximum_size_100000_test_data))]
    public void Directories_of_maximum_size_100000(string terminalInput, int size)
    {
        Assert.That(
            () =>
            {
                var (_, allDirectories) = MapFilesystem(terminalInput.Split(Environment.NewLine).ToArray());
                return allDirectories.Where(dir => dir.Size <= 100_000).Sum(dir => dir.Size);
            },
            Is.EqualTo(size));
    }

    private static IEnumerable Smallest_directory_to_be_removed_test_data()
    {
        yield return new TestCaseData
            (System.IO.File.ReadAllText("test.txt"), 24933642);
        
        yield return new TestCaseData
            (System.IO.File.ReadAllText("input.txt"), 1498966);
    }

    [Test, TestCaseSource(nameof(Smallest_directory_to_be_removed_test_data))]
    public void Smallest_directory_to_be_removed(string terminalInput, int size)
    {
        Assert.That(
            () =>
            {
                var (_, allDirectories) = MapFilesystem(terminalInput.Split(Environment.NewLine).ToArray());
                var unused = 70_000_000 - allDirectories.Single(dir => dir.ParentDirectory is null).Size;
                return allDirectories.Where(dir => dir.Size >= (30_000_000 - unused)).Min(dir => dir.Size);
            },
            Is.EqualTo(size));
    }

    private static (Directory, IList<Directory>) MapFilesystem(IEnumerable<string> commands)
    {
        var currentDirectory = new Directory("/", null);
        var allDirectories = new List<Directory>() { currentDirectory };
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
                    allDirectories.Add(dir);
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

        return (GetBaseDirectory(currentDirectory!), allDirectories);
    }
}