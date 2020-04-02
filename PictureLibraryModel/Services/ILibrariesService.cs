using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services
{
    public interface ILibrariesService
    {
        /// <summary>
        /// Creates library file in specified directory
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="name"></param>
        void CreateLibrary(string directoryPath, string name);
    }
}
