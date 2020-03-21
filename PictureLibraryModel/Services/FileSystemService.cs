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

        public FileSystemService()
        {

        }

        public void CopyFile(string sourceFilePath, string destinationFilePath, bool overwrite) 
        {
            File.Copy(sourceFilePath, destinationFilePath,overwrite);
        }

        public List<string> GetAllDirectories(string topDirectory, SearchOption option)
        {
            if (System.IO.Directory.Exists(topDirectory))
            {
                var directories = System.IO.Directory.GetDirectories(topDirectory, "*", option);
                return directories.ToList<string>();
            }
            else
            {
                return null;
            }
        }

        public List<ImageFile> GetAllImageFiles(string directory)
        {
            var files = System.IO.Directory.GetFiles(directory, "*");
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

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
