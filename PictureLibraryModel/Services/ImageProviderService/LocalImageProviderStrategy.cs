using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public class LocalImageProviderStrategy : IImageProviderStrategy
    {
        private IFileService FileService { get; }

        public LocalImageProviderStrategy(IFileService fileService)
        {
            FileService = fileService;
        }

        public Task AddImageToLibraryAsync(ImageFile imageFile, byte[] image)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GetImageAsync(ImageFile imageFile)
        {
            return await Task.Run(() => FileService.ReadAllBytes(imageFile.FullName));
        }

        public Task LoadImagesIconsAsync(IEnumerable<ImageFile> imageFiles)
        {
            throw new NotSupportedException();
        }

        public Task RemoveImageAsync(ImageFile imageFile)
        {
            throw new NotImplementedException();
        }

        public Task UpdateImageAsync(ImageFile imageFile, byte[] image)
        {
            throw new NotImplementedException();
        }
    }
}
