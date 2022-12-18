using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NoSpaceLeftOnDevice;

internal class Directory : IFilesystem
{
    public Directory(string name, Directory? parentDirectory)
    {
        Name = name;
        ParentDirectory = parentDirectory;
    }

    public int Size => Content.Sum(f => f.Size);

    public string Name { get; }

    public Directory? ParentDirectory { get; }

    public IList<IFilesystem> Content { get; } = new List<IFilesystem>();

    public IEnumerable<Directory> Directories => Content.OfType<Directory>();

    public IEnumerable<File> Files => Content.OfType<File>();

    public override string ToString()
    {
        static int Indentation(Directory? directory, int indentation = 0)
        {
            if (directory is null)
                return indentation;

            return Indentation(directory.ParentDirectory, indentation + 1);
        }

        var indentation = Indentation(ParentDirectory);
        var print = $"{new string("-").PadLeft(indentation*3, ' ')} {Name} (dir, size={Size})";
        foreach (var c in Content.OrderBy(c => c.Name))
        {
            print += Environment.NewLine;
            switch (c)
            {
                case Directory directory:
                    print += directory.ToString();
                    break;
                case File file:
                    print += $"{new string("-").PadLeft((indentation+1)*3, ' ')} {file.Name} (file, size={file.Size})";
                    break;
            }
        }

        return print;
    }
}