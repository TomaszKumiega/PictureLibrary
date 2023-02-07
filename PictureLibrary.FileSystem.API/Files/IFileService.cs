namespace PictureLibrary.FileSystem.API
{
    public interface IFileService
    {
        bool Exists(string path);
        void Create(string path);
        Stream Open(string path);
    }
}
