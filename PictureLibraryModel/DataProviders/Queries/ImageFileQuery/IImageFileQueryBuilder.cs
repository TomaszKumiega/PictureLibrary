using PictureLibraryModel.Model;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders.Queries
{
    public interface IImageFileQueryBuilder
    {
        List<ImageFile> ToList();
    }
}