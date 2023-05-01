namespace PictureLibrary.FileSystem.API.Directories
{
    public interface IDirectoryService
    {
        void Create(string path);
        DirectoryInfo GetDirectoryInfo(string path);
    }
}
