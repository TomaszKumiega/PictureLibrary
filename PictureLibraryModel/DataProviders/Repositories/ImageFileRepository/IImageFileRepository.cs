using PictureLibraryModel.DataProviders.Queries;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders.Repositories
{
    public interface IImageFileRepository
    {
        ImageFile AddImageToLibrary(ImageFile imageFile, Library library);
        IImageFileQueryBuilder Query();
        void RemoveImage(ImageFile imageFile);
    }
}