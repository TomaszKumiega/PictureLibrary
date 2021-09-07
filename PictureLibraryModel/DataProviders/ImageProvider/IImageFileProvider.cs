using PictureLibraryModel.Model;
using System.Drawing;

namespace PictureLibraryModel.DataProviders
{
    public interface IImageFileProvider
    {
        ImageFile AddImageToLibrary(ImageFile imageFile, string libraryFullName);
        byte[] GetImageAsync(ImageFile imageFile);
        Icon LoadImageIcon(ImageFile imageFile);
        void RemoveImage(ImageFile imageFile);
    }
}