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

        public string CurrentDirectory { get; }

        public FileSystemService()
        {
            CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public void CopyFile(string sourceFilePath, string destinationFilePath, bool overwrite) 
        {
            File.Copy(sourceFilePath, destinationFilePath,overwrite);
        }

        public List<string> GetAllDirectories(SearchOption option)
        {
            var directories = Directory.GetDirectories(CurrentDirectory, "*", option);
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
            var files = Directory.GetFiles(CurrentDirectory, "*");
            var listOfFiles = files.ToList<string>();
            var listOfImageFiles = new List<ImageFile>();

            foreach(var t in listOfFiles)
            {
                if (!ImageFile.IsFileAnImage(t))
                {
                    listOfFiles.Remove(t);
                }
                else
                {
                    listOfImageFiles.Add(new ImageFile(t));
                }
            }

            return listOfImageFiles;
        }

        public void MoveFile(string filePath, string destinationPath, bool overwrite)
        {
            File.Move(filePath, destinationPath, overwrite);        
        }

        public void RemoveFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
