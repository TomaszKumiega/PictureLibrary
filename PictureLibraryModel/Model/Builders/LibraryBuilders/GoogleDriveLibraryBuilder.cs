using System;

namespace PictureLibraryModel.Model.Builders
{
    public class GoogleDriveLibraryBuilder : IRemoteLibraryBuilder
    {
        private readonly Func<GoogleDriveLibrary> _libraryLocator;

        private GoogleDriveLibrary _library;

        public GoogleDriveLibraryBuilder(Func<GoogleDriveLibrary> libraryLocator)
        {
            _libraryLocator = libraryLocator;
        }

        public Library Build()
        {
            return _library;
        }

        public IRemoteLibraryBuilder CreateLibrary()
        {
            _library = _libraryLocator();

            return this;
        }

        public IRemoteLibraryBuilder WithDescription(string description)
        {
            _library.Description = description;

            return this;
        }

        public IRemoteLibraryBuilder WithName(string name)
        {
            _library.Name = name;

            return this;
        }

        public IRemoteLibraryBuilder WithPath(string path)
        {
            return this;
        }

        public IRemoteLibraryBuilder WithRemoteStorageInfo(Guid? remoteStorageInfoId)
        {
            _library.DataStoreInfoId = remoteStorageInfoId.Value;

            return this;
        }
    }
}
