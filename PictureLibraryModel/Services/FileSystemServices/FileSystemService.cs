using PictureLibraryModel.Model;
using System.IO;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public abstract class FileSystemService : IFileSystemService
    {
        public abstract void Copy(string sourcePath, string destinationPath);
        public abstract void Create(string path);

        public abstract bool Exists(string path);

        public abstract FileSystemInfo GetInfo(string path);

        public abstract void Move(string sourcePath, string destinationPath);

        public abstract void Remove(string path);

        public abstract void Rename(string path, string name);
    }
}
