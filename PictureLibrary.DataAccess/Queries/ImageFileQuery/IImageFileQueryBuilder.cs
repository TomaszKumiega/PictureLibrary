using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Queries.ImageFileQuery
{
    public interface IImageFileQueryBuilder
    {
        List<ImageFile> ToList();
    }
}