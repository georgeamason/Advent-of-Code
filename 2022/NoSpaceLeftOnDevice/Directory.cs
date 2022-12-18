using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NoSpaceLeftOnDevice;

internal class Directory : IFilesystem
{
    public Directory(string name, string[] commands)
    {
        _commands = commands;
        Name = name;
    }

    public int Size => Contents.Sum(c => c.Size);

    public string Name { get; }

    public IEnumerable<Directory> Directories => Contents.OfType<Directory>().ToList();

    public IEnumerable<File> Files => Contents.OfType<File>().ToList();

    private IEnumerable<IFilesystem> Contents
    {
        get
        {
            foreach (var cmd in _commands)
            {
                if (cmd.StartsWith("dir"))
                {
                    //yield return Task.Factory.StartNew(() => Test(_commands, cmd)).Result;
                }
                else if (int.TryParse(cmd.Split(" ")[0], out var size))
                {
                    var filename = cmd.Split(" ")[1];
                    yield return new File(filename, size);
                }
            }
        }
    }

    private readonly string[] _commands;
}