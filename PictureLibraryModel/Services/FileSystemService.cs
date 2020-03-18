using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

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

        public void CopyFile(string sourceFilePath, string destinationFilePath, bool overwrite) 
        {
            File.Copy(sourceFilePath, destinationFilePath,overwrite);
        }

        public List<string> GetAllDirectories(SearchOption option)
        {
            var directories = Directory.GetDirectories(CurrentPath, "*", option);
            return directories.ToList<string>();
        }

        public List<string> GetAllDirectories(string topDirectory, SearchOption option)
        {
            if (Directory.Exists(topDirectory))
            {
                var directories = Directory.GetDirectories(topDirectory, "*", option);
                return directories.ToList<string>();
            }
            else
            {
                return null;
            }
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
