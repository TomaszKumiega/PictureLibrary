using System;

namespace PictureLibraryModel.Model.Builders
{
    public class LocalLibraryBuilder : ILocalLibraryBuilder
    {
        private readonly Func<Library> _libraryLocator;

        private Library _library;

        public LocalLibraryBuilder(Func<LocalLibrary> libraryLocator)
        {
            _libraryLocator = libraryLocator;
        }

        public ILocalLibraryBuilder CreateLibrary()
        {
            _library = _libraryLocator();

            return this;
        }

        public ILocalLibraryBuilder WithDescription(string description)
        {
            _library.Description = description;

            return this;
        }

        public ILocalLibraryBuilder WithName(string name)
        {
            _library.Name = name;

            return this;
        }

        public ILocalLibraryBuilder WithPath(string path)
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


