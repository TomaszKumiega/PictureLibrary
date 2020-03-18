using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PictureLibraryModel.Services
{
    public class FileSystemService : IFileSystemService
    {

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public string CurrentPath { get; }

        public FileSystemService()
        {
            CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public void CopyFile(string filePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllCatalogsPathsList()
        {
            throw new NotImplementedException();
        }

        public List<ImageFile> GetAllImageFiles()
        {
            throw new NotImplementedException();
        }

        public void MoveFile(string filePath, string destinationPath, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
