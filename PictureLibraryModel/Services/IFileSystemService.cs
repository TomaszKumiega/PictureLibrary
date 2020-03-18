using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Services
{
    /// <summary>
    /// Defines file system control logic
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// Containes path of catalog, object is currently operating on
        /// </summary>
        string CurrentPath { get; }

        /// <summary>
        /// Provides a list of catalogs in current directory as well as subcatalogs
        /// </summary>
        /// <returns></returns>
        List<string> GetAllDirectories(SearchOption option);

        /// <summary>
        /// Provides a list of directories in specified directory 
        /// </summary>
        /// <returns></returns>
        List<string> GetAllDirectories(string topDirectory, SearchOption option);

        /// <summary>
        /// Provides a list of image files in current directory
        /// </summary>
        /// <returns></returns>
        List<ImageFile> GetAllImageFiles();

        /// <summary>
        /// Copies file to the specified destination
        /// </summary>
        void CopyFile(string filePath, string destinationPath, bool overwrite);

        /// <summary>
        /// Moves file to the specified destination 
        /// </summary>
        void MoveFile(string filePath, string destinationPath, bool overwrite);

        /// <summary>
        /// Permanently removes file 
        /// </summary>
        /// <param name="filePath"></param>
        void RemoveFile(string filePath);
    }
}
