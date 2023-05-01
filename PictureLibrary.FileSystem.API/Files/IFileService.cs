namespace PictureLibrary.FileSystem.API
{
    public interface IFileService
    {
        bool Exists(string path);
        Stream Create(string path);
        Stream Open(string path);
        bool Delete(string path);
        FileInfo GetFileInfo(string path);
    }
}
