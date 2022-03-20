using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.IO;

namespace PictureLibraryModel.DataProviders
{
    public class LocalImageFileProvider : IImageFileProvider
    {
        private IFileService FileService { get; }
        private IDirectoryService DirectoryService { get; }
        private Func<LocalImageFile> ImageFileLocator { get; }

        public LocalImageFileProvider(IFileService fileService, IDirectoryService directoryService, Func<LocalImageFile> imageFileLocator)
        {
            FileService = fileService;
            DirectoryService = directoryService;
            ImageFileLocator = imageFileLocator;
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, string libraryFullName)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));
            if (string.IsNullOrEmpty(libraryFullName))
                throw new ArgumentException(nameof(libraryFullName));

            var directory = DirectoryService.GetParent(libraryFullName);
            var path = directory.Path + "\\Images\\" + imageFile.Name;

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
