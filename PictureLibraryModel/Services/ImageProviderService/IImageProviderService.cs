using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public interface IImageProviderService
    {
        Task AddImageToLibraryAsync(ImageFile imageFile, byte[] image);
        Task<byte[]> GetImageAsync(ImageFile imageFile);
        Task UpdateImageAsync(ImageFile imageFile, byte[] image);
        Task LoadImagesIconsAsync(IEnumerable<ImageFile> imageFiles);
        Task RemoveImageAsync(ImageFile imageFile);
    }
}
