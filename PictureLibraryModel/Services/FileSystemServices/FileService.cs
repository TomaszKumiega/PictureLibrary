using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public class FileService : IFileService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Copy(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath);
        }

        public void Create(string path)
        {
            File.Create(path);
        }

        public FileSystemInfo GetInfo(string path)
        {
            return new FileInfo(path);
        }

        public void Move(string sourcePath, string destinationPath)
        {
            File.Move(sourcePath, destinationPath);
        }

        public FileStream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open);
        }

        public void Remove(string path)
        {
            throw new NotImplementedException();
        }

        public void Rename(string path, string name)
        {
            var extension = new FileInfo(path).Extension;
            var directoryPath = Directory.GetParent(path).FullName;

            File.Move(path, directoryPath + "\\" + name + extension);
        }
    }
}
