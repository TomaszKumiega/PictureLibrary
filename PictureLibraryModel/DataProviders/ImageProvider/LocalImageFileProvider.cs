using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;

namespace PictureLibraryModel.DataProviders
{
    public class LocalImageFileProvider : IImageFileProvider
    {
        private readonly IFileService _fileService;
        private readonly IDirectoryService _directoryService;
        private readonly Func<LocalImageFile> _imageFileLocator;

        public LocalImageFileProvider(IFileService fileService, IDirectoryService directoryService, Func<LocalImageFile> imageFileLocator)
        {
            _fileService = fileService;
            _directoryService = directoryService;
            _imageFileLocator = imageFileLocator;
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, string libraryFullName)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));
            if (string.IsNullOrEmpty(libraryFullName))
                throw new ArgumentException(null, nameof(libraryFullName));

            var directory = _directoryService.GetParent(libraryFullName);
            var path = directory.Path + "\\Images\\" + imageFile.Name;

            _fileService.Copy(imageFile.Path, path);

            var fileInfo = _fileService.GetInfo(path);

            var newImageFile = _imageFileLocator();

            newImageFile.Name = fileInfo.Name;
            newImageFile.Path = fileInfo.FullName;
            newImageFile.Extension = fileInfo.Extension;
            newImageFile.LibraryFullName = libraryFullName;

            return newImageFile;
        }

        public byte[] GetImageAsync(ImageFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            return _fileService.ReadAllBytes(imageFile.Path);
        }

        public void RemoveImage(ImageFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            _fileService.Remove(imageFile.Path);
        }
    }
}
