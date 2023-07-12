using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.Builders
{
    public class ImageFileBuilder : IImageFileBuilder
    {
        private ImageFile? _imageFile;

        public IImageFileBuilder CreateImageFile(IDataStoreInfo? dataStoreInfo = null, string? path = null)
        {
            if (dataStoreInfo == null)
            {
                _imageFile = new LocalImageFile()
                {
                    Id = Guid.NewGuid(),
                    Path = path,
                };
            }
            else if (dataStoreInfo is GoogleDriveDataStoreInfo)
            {
                _imageFile = new GoogleDriveImageFile()
                {
                    Id = Guid.NewGuid(),
                    DataStoreInfoId = dataStoreInfo.Id,
                };
            }
            else if (dataStoreInfo is ApiDataStoreInfo)
            {
                _imageFile = new ApiImageFile()
                {
                    Id = Guid.NewGuid(),
                    DataStoreInfoId = dataStoreInfo.Id,
                };
            }
            else
            {
                throw new InvalidOperationException("Library type for specified data store info is not supported");
            }

            return this;
        }

        public IImageFileBuilder WithExtension(string extension)
        {
            _imageFile!.Extension = extension;

            return this;
        }

        public IImageFileBuilder WithName(string name)
        {
            _imageFile!.Name = name;

            return this;
        }

        public ImageFile GetImageFile()
        {
            return _imageFile ?? throw new InvalidOperationException("Cannot return library without creating it first.");
        }
    }
}
