using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryModel.DataProviders
{
    public class LocalImageProvider : IImageProvider
    {
        private IFileService FileService { get; }
        private IImageFileBuilder ImageFileBuilder { get; }

        public LocalImageProvider(IFileService fileService, IImageFileBuilder imageFileBuilder)
        {
            FileService = fileService;
            ImageFileBuilder = imageFileBuilder;
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, string libraryFullName)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));
            if (string.IsNullOrEmpty(libraryFullName))
                throw new ArgumentException(nameof(libraryFullName));

            Directory directory = FileService.GetParent(libraryFullName);
            var path = directory.FullName + "icons\\" + imageFile.Name;

            FileService.Copy(imageFile.FullName, path);

            var fileInfo = FileService.GetInfo(path);

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
                    .WithSize((fileInfo as FileInfo)?.Length ?? 0)
                    .From(Guid.Empty)
                    .Build();

            return newImageFile;
        }

        public byte[] GetImageAsync(ImageFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            return FileService.ReadAllBytes(imageFile.FullName);
        }

        public Icon LoadImageIcon(ImageFile imageFile)
        {
            throw new NotSupportedException();
        }

        public void RemoveImage(ImageFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            FileService.Remove(imageFile.FullName);
        }
    }
}
