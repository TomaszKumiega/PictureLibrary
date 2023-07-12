using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using System.Collections.Generic;

namespace PictureLibraryModel.Builders
{
    public interface IImageFileBuilder
    {
        IImageFileBuilder CreateImageFile(IDataStoreInfo? dataStoreInfo = null, string? path = null);
        IImageFileBuilder WithExtension(string extension);
        IImageFileBuilder WithName(string name);
        ImageFile GetImageFile();
    }
}
