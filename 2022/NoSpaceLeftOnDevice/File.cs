namespace NoSpaceLeftOnDevice;

internal class File : IFilesystem
{
    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }

    public int Size { get; }
}