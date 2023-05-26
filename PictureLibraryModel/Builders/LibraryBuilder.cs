using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using System;

namespace PictureLibraryModel.Builders
{
    public class LibraryBuilder : ILibraryBuilder
    {
        private Library? _library;

        public ILibraryBuilder CreateLibrary(IDataStoreInfo? dataStoreInfo = null)
        {
            if (dataStoreInfo == null)
            {
                _library = new LocalLibrary();
            }
            else if (dataStoreInfo is GoogleDriveDataStoreInfo)
            {
                _library = new GoogleDriveLibrary()
                {
                    DataStoreInfoId = dataStoreInfo.Id,
                };
            }
            else if (dataStoreInfo is ApiDataStoreInfo)
            {
                _library = new ApiLibrary()
                {
                    DataStoreInfoId = dataStoreInfo.Id,
                };
            }
            else
            {
                throw new InvalidOperationException("Library type for specified data store info is not supported");
            }

            return this;
        }

        public Library GetLibrary()
        {
            return _library ?? throw new NotSupportedException($"Cannot retrieve library without calling {nameof(CreateLibrary)} method.");
        }

        public ILibraryBuilder WithDescription(string? description)
        {
            _library!.Description = description;

            return this;
        }

        public ILibraryBuilder WithName(string name)
        {
            _library!.Name = name;

            return this;
        }
    }
}
