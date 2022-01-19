using System;

namespace PictureLibraryModel.Model.Builders
{
    public class LocalLibraryBuilder : ILibraryBuilder
    {
        private Library Library { get; set; }
        private Func<Library> LibraryLocator { get; }

        public LocalLibraryBuilder(Func<Library> libraryLocator)
        {
            LibraryLocator = libraryLocator;
        }

        public ILibraryBuilder CreateLibrary()
        {
            Library = LibraryLocator();

            return this;
        }

        public ILibraryBuilder WithDescription(string description)
        {
            Library.Description = description;

            return this;
        }

        public ILibraryBuilder WithName(string name)
        {
            Library.Name = name;

            return this;
        }

        public ILibraryBuilder WithPath(string path)
        {
            Library.Path = path;

            return this;
        }

        public ILibraryBuilder WithStorageInfoId(Guid? storageInfoId)
        {
            Library.RemoteStorageInfoId = storageInfoId;

            return this;
        }

        public Library Build()
        {
            return Library;
        }
    }
}
