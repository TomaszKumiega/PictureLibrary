namespace PictureLibrary.FileSystem.API.Directories
{
    public class DirectoryService : IDirectoryService
    {
        #region Public methods
        public void Create(string path)
        {
            _ = Directory.CreateDirectory(path);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }

        public DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(path);
        }
        #endregion
    }
}
