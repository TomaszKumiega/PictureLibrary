using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public interface IImageProviderContext
    {
        IImageProviderStrategy Strategy { get; set; }

        Task<ImageFile> AddImageToLibraryAsync(ImageFile imageFile, string libraryFullName);
        Task<byte[]> GetImageAsync(ImageFile imageFile);
        Task UpdateImageAsync(ImageFile imageFile, byte[] image);
        Task LoadImageIcon(ImageFile imageFile);
        Task RemoveImageAsync(ImageFile imageFile);
    }
}
