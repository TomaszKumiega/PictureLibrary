using PictureLibraryModel.Model;
using PictureLibraryModel.Resources;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.IO;

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

        public ImageFile AddImageToLibrary(ImageFile imageFile, Library library)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));
            if (library is not LocalLibrary)
                throw new InvalidOperationException("Invalid library type");

            Model.Directory directory = _directoryService.GetParent(library.Path);
            string path = directory.Path + Path.DirectorySeparatorChar + Strings.ImagesDirectory + Path.DirectorySeparatorChar + imageFile.Name;

            _fileService.Copy(imageFile.Path, path);

            FileSystemInfo fileInfo = _fileService.GetInfo(path);

            LocalImageFile newImageFile = _imageFileLocator();

            newImageFile.Name = fileInfo.Name;
            newImageFile.Path = fileInfo.FullName;
            newImageFile.Extension = fileInfo.Extension;
            newImageFile.LibraryFullName = library.Path;

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
