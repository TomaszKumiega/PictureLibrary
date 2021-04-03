using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public abstract class FileSystemService : IFileSystemService
    {
        public abstract void Copy(string sourcePath, string destinationPath);
        public abstract void Create(string path);

        public abstract bool Exists(string path);

        public abstract FileSystemInfo GetInfo(string path);

        public virtual Model.Directory GetParent(string path)
        {
            var directoryInfo = System.IO.Directory.GetParent(path);

            var parentDirectory = new Folder(new DirectoryService(new ImageFileBuilder()), directoryInfo);

            return parentDirectory;
        }

        public abstract void Move(string sourcePath, string destinationPath);

        public abstract void Remove(string path);

        public abstract void Rename(string path, string name);
    }
}
