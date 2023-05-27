namespace PictureLibrary.FileSystem.API.Directories
{
    public interface IDirectoryService
    {
        void Create(string path);
        void DeleteDirectory(string path);
        DirectoryInfo GetDirectoryInfo(string path);
    }
}
