using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.DataProviders
{
    public interface ILibraryProvider
    {
        void AddLibrary(Library library);
        void AddLibraries(IEnumerable<Library> libraries);
        IEnumerable<Library> GetAllLibraries();
        void RemoveLibrary(Library library);
        void UpdateLibrary(Library library);
    }
}
