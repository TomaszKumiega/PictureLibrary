using System;
using System.Collections.Generic;
using System.Text;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.Services
{
    public interface ILibraryFileService
    {
        /// <summary>
        /// Creates library file in specified directory
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="name"></param>
        void CreateLibrary(string directoryPath, string name);

        /// <summary>
        /// Returns list of all picture libraries on a device
        /// </summary>
        /// <returns></returns>
        List<Library> GetAllLibraries();

        /// <summary>
        /// Saves list of libraries 
        /// </summary>
        /// <param name="libraries"></param>
        void SaveLibraries(List<Library> libraries);

    }
}
