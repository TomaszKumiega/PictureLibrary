using PictureLibraryModel.Model;
using System.Drawing;

namespace PictureLibrary.DataAccess.ImageProvider
{
    public interface IImageFileProvider
    {
        ImageFile AddImageToLibrary(ImageFile imageFile, Library library);
        byte[] GetImageAsync(ImageFile imageFile);
        void RemoveImage(ImageFile imageFile);
    }
}