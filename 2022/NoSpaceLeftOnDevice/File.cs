namespace NoSpaceLeftOnDevice;

internal class File : IFilesystem
{
    public File(string filename, int size)
    {
        Filename = filename;
        Size = size;
    }

    private string Filename { get; }

    public int Size { get; }
}