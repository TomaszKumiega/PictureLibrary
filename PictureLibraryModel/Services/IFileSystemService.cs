using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

namespace PictureLibraryModel.Services
{
    public interface IFileSystemService
    {
        /// <summary>
        /// Finds all files matching the search pattern from all drives
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        List<string> FindFiles(string searchPattern, string directory);

        /// <summary>
        /// Creates a file and returns FileStream
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        FileStream CreateFile(string filePath);

        /// <summary>
        /// Creates a file and writes all bytes to it. Returns path to the file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        /// <returns>Path of the file</returns>
        string AddFile(string filePath, byte[] file);

        /// <summary>
        /// Deletes specified file 
        /// </summary>
        /// <param name="filePath"></param>
        void DeleteFile(string filePath);

        /// <summary>
        /// Returns a FileStream of a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        FileStream OpenFile(string path, FileMode mode);

        /// <summary>
        /// Returns contents of a file as byte array
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        byte[] GetFile(string path);

        /// <summary>
        /// Renames the file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newName"></param>
        void RenameFile(string file, string newName);

        /// <summary>
        /// Provides file info of specified file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        FileInfo GetFileInfo(string path);

        /// <summary>
        /// Extracts icon from a specified file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Icon ExtractAssociatedIcon(string path);
    }
}
