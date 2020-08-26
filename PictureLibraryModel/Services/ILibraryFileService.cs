using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.Services
{
    public interface ILibraryFileService
    {
        /// <summary>
        /// Creates library file in specified directory
        /// </summary>
       Library CreateLibrary(string libraryName, FileStream fileStream);

        /// <summary>
        /// Returns list of all picture libraries on a device
        /// </summary>
        /// <returns></returns>
        Task<ObservableCollection<Library>> GetAllLibrariesAsync();

        /// <summary>
        /// Saves list of libraries 
        /// </summary>
        /// <param name="libraries"></param>
        Task SaveLibrariesAsync(List<Library> libraries);

        /// <summary>
        /// Loads contains of specified library file 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>Object of <see cref="Library"></see> class/></returns>
        Task<Library> LoadLibraryAsync(FileStream fileStream);

    }
}
