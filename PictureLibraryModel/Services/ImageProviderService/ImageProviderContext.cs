using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public class ImageProviderContext : IImageProviderContext
    {
        public IImageProviderStrategy Strategy { get; set; }

        public async Task<ImageFile> AddImageToLibraryAsync(ImageFile imageFile, string libraryFullName)
        {
            return await Strategy.AddImageToLibraryAsync(imageFile, libraryFullName);
        }

        public async Task<byte[]> GetImageAsync(ImageFile imageFile)
        {
            return await Strategy.GetImageAsync(imageFile);
        }

        public async Task LoadImageIcon(ImageFile imageFile)
        {
            await Strategy.LoadImageIcon(imageFile);
        }

        public async Task RemoveImageAsync(ImageFile imageFile)
        {
            await Strategy.RemoveImageAsync(imageFile);
        }

        public async Task UpdateImageAsync(ImageFile imageFile, byte[] image)
        {
            await Strategy.UpdateImageAsync(imageFile, image);
        }
    }
}
