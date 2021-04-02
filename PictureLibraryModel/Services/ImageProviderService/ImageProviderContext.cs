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

        public async Task<ImageFile> AddImageToLibraryAsync(ImageFile imageFile, string libraryDirectory)
        {
            return await Strategy.AddImageToLibraryAsync(imageFile, libraryDirectory);
        }

        public async Task<byte[]> GetImageAsync(ImageFile imageFile)
        {
            return await Strategy.GetImageAsync(imageFile);
        }

        public async Task LoadImagesIconsAsync(IEnumerable<ImageFile> imageFiles)
        {
            await Strategy.LoadImagesIconsAsync(imageFiles);
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
