using PictureLibraryModel.Model;
using System.Drawing;

namespace PictureLibraryModel.DataProviders
{
    public interface IImageFileProvider
    {
        ImageFile AddImageToLibrary(ImageFile imageFile, Library library);
        byte[] GetImageAsync(ImageFile imageFile);
        void RemoveImage(ImageFile imageFile);
    }
}