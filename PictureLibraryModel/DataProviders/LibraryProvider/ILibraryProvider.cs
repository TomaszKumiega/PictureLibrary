using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders
{
    public interface ILibraryProvider
    {
        void AddLibrary(Library library);
        void AddLibraries(IEnumerable<Library> libraries);
        IEnumerable<Library> GetAllLibraries();
        void RemoveLibrary(Library library);
        void UpdateLibrary(Library library);
        Library GetLibrary(string name);
        Library FindLibrary(Predicate<Library> predicate);
    }
}
