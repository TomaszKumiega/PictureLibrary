using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public interface IImageProviderStrategy
    {
        Task<ImageFile> AddImageToLibraryAsync(ImageFile imageFile, string libraryFullName);
        Task<byte[]> GetImageAsync(ImageFile imageFile);
        Task UpdateImageAsync(ImageFile imageFile, byte[] image);
        Task LoadImagesIconsAsync(IEnumerable<ImageFile> imageFiles);
        Task RemoveImageAsync(ImageFile imageFile);
    }
}
