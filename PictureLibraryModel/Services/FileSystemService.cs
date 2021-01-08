using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryModel.Services
{
    public abstract class FileSystemService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Finds all files matching the search pattern from all drives
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public List<string> FindFiles(string searchPattern, string directory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a file and returns FileStream
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public FileStream CreateFile(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a file and writes all bytes to it. Returns path to the file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        /// <returns>Path of the file</returns>
        public string AddFile(string filePath, byte[] file)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes specified file 
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a FileStream of a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileStream OpenFile(string path, FileMode mode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns contents of a file as byte array
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFile(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Renames the file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newName"></param>
        public void RenameFile(string file, string newName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides file info of specified file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileInfo GetFileInfo(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extracts icon from a specified file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Icon ExtractAssociatedIcon(string path)
        {
            return Icon.ExtractAssociatedIcon(path);
        }

        public IEnumerable<Directory> GetDirectories(string topDirectory, SearchOption option)
        {
            if (topDirectory == null) throw new ArgumentNullException();


            if (!System.IO.Directory.Exists(topDirectory)) throw new DirectoryNotFoundException("Directory: " + topDirectory + " not found");


            string[] fullPaths = null;

            try
            {
                fullPaths = System.IO.Directory.GetDirectories(topDirectory, "*", option);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Couldn't load directories from " + topDirectory);
            }

            var directories = new List<Directory>();

            if (fullPaths != null)
            {
                foreach (var t in fullPaths)
                {
                    directories.Add(new Directory(t, (new System.IO.DirectoryInfo(t)).Name, this));
                }
            }
            else
            {
                throw new Exception("Failed getting directories");
            }

            return directories;
        }
    }
}
