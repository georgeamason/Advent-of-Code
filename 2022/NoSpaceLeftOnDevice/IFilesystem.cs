namespace NoSpaceLeftOnDevice;

public interface IFilesystem
{
    public int Size { get; }

    public string Name { get; }
}