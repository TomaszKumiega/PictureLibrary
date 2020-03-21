using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services
{
    /// <summary>
    /// Defines file system control logic
    /// </summary>
    public interface IFileSystemService
    {

        /// <summary>
        /// Provides a list of directories in specified directory 
        /// </summary>
        /// <returns></returns>
        Task<ObservableCollection<Model.Directory>> GetAllDirectories(string topDirectory, SearchOption option);

        /// <summary>
        /// Provides a list of image files in current directory
        /// </summary>
        /// <returns></returns>
        List<ImageFile> GetAllImageFiles(string directory);

        /// <summary>
        /// Provides an observable collection of drives 
        /// </summary>
        /// <returns></returns>
        ObservableCollection<Drive> GetDrives();

        /// <summary>
        /// Copies file to the specified destination
        /// </summary>
        void CopyFile(string filePath, string destinationPath, bool overwrite);

        /// <summary>
        /// Moves file to the specified destination 
        /// </summary>
        void MoveFile(string filePath, string destinationPath, bool overwrite);

        /// <summary>
        /// Deletes specified file 
        /// </summary>
        /// <param name="filePath"></param>
        void DeleteFile(string filePath);
    }
}
