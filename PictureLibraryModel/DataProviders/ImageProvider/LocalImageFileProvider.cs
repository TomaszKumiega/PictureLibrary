using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Drawing;
using System.IO;
using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryModel.DataProviders
{
    public class LocalImageFileProvider : IImageFileProvider
    {
        private IFileService FileService { get; }
        private Func<LocalImageFile> ImageFileLocator { get; }

        public LocalImageFileProvider(IFileService fileService, Func<LocalImageFile> imageFileLocator)
        {
            FileService = fileService;
            ImageFileLocator = imageFileLocator;
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, string libraryFullName)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));
            if (string.IsNullOrEmpty(libraryFullName))
                throw new ArgumentException(nameof(libraryFullName));

            string directory = FileService.GetParent(libraryFullName);
            var path = directory + "icons\\" + imageFile.Name;

            FileService.Copy(imageFile.Path, path);

            var fileInfo = FileService.GetInfo(path);

            var newImageFile = ImageFileLocator();

            newImageFile.Name = fileInfo.Name;
            newImageFile.Path = fileInfo.FullName;
            newImageFile.Extension = ImageExtensionHelper.GetExtension(fileInfo.Extension);
            newImageFile.LibraryFullName = libraryFullName;

            return newImageFile;
        }

        public byte[] GetImageAsync(ImageFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            return FileService.ReadAllBytes(imageFile.Path);
        }

        public void RemoveImage(ImageFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            FileService.Remove(imageFile.Path);
        }
    }
}
