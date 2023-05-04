namespace PictureLibrary.FileSystem.API.Files
{
    public class FileService : IFileService
    {
        #region Public methods
        public Stream Create(string path)
        {
            return File.Create(path);
        }

        public bool Delete(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public FileInfo GetFileInfo(string path)
        {
            return new FileInfo(path);
        }

        public Stream Open(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        #endregion
    }
}
