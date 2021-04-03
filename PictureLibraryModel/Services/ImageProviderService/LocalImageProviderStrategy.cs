using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public class LocalImageProviderStrategy : IImageProviderStrategy
    {
        private IFileService FileService { get; }
        private IImageFileBuilder ImageFileBuilder { get; }

        public LocalImageProviderStrategy(IFileService fileService, IImageFileBuilder imageFileBuilder)
        {
            FileService = fileService;
            ImageFileBuilder = imageFileBuilder;
        }

        public async Task<ImageFile> AddImageToLibraryAsync(ImageFile imageFile, string libraryFullName)
        {
            Model.Directory directory;

            try
            {
                directory = FileService.GetParent(libraryFullName);
            }
            catch(Exception)
            {
                throw new Exception("Error finding library directory: " + libraryFullName);
            }

            var path = directory + imageFile.Name;

            await Task.Run(() => FileService.Copy(imageFile.FullName, path));

            var fileInfo = new FileInfo(path);
           
            var newImageFile =
                ImageFileBuilder
                .StartBuilding()
                .WithName(fileInfo.Name)
                .WithFullName(fileInfo.FullName)
                .WithExtension(fileInfo.Extension)
                .WithCreationTime(fileInfo.CreationTime)
                .WithLastAccessTime(fileInfo.LastAccessTime)
                .WithLastWriteTime(fileInfo.LastWriteTime)
                .WithLibraryFullName(libraryFullName)
                .WithSize(fileInfo.Length)
                .WithTags(new List<Tag>())
                .From(Origin.Local)
                .Build();

            return newImageFile;
        }

        public async Task<byte[]> GetImageAsync(ImageFile imageFile)
        {
            return await Task.Run(() => FileService.ReadAllBytes(imageFile.FullName));
        }

        public Task LoadImagesIconsAsync(IEnumerable<ImageFile> imageFiles)
        {
            throw new NotSupportedException();
        }

        public async Task RemoveImageAsync(ImageFile imageFile)
        {
            await Task.Run(() => FileService.Remove(imageFile.FullName));
        }

        public async Task UpdateImageAsync(ImageFile imageFile, byte[] image)
        {
            await Task.Run(() => FileService.WriteAllBytes(imageFile.FullName, image));
        }
    }
}
