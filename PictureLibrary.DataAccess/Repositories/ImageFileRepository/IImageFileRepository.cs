using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Repositories.ImageFileRepository
{
    public interface IImageFileRepository
    {
        ImageFile AddImageToLibrary(ImageFile imageFile, Library library);
        void RemoveImage(ImageFile imageFile);
    }
}