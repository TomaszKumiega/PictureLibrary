using System;

namespace PictureLibraryModel.Model.Builders
{
    public class LocalLibraryBuilder : ILibraryBuilder
    {
        private readonly Func<Library> _libraryLocator;

        private Library _library;

        public LocalLibraryBuilder(Func<LocalLibrary> libraryLocator)
        {
            _libraryLocator = libraryLocator;
        }

        public ILibraryBuilder CreateLibrary()
        {
            _library = _libraryLocator();

            return this;
        }

        public ILibraryBuilder WithDescription(string description)
        {
            _library.Description = description;

            return this;
        }

        public ILibraryBuilder WithName(string name)
        {
            _library.Name = name;

            return this;
        }

        public ILibraryBuilder WithPath(string path)
        {
            _library.Path = path;

            return this;
        }

        public Library Build()
        {
            return _library;
        }
    }
}


