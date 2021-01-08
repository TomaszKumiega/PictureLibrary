using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

namespace PictureLibraryModel.Services
{
    public abstract class FileSystemService
    {
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
            throw new NotImplementedException();
        }
    }
}
