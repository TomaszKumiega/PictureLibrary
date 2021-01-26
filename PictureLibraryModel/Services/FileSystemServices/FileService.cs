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
            throw new NotImplementedException();
        }

        public void Move(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public FileStream OpenFile(string path)
        {
            throw new NotImplementedException();
        }

        public void Rename(string path, string name)
        {
            throw new NotImplementedException();
        }
    }
}
