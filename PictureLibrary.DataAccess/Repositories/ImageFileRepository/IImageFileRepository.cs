using PictureLibrary.DataAccess.Queries.ImageFileQuery;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Repositories.ImageFileRepository
{
    public interface IImageFileRepository
    {
        ImageFile AddImageToLibrary(ImageFile imageFile, Library library);
        IImageFileQueryBuilder Query();
        void RemoveImage(ImageFile imageFile);
    }
}