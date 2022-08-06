using PictureLibraryModel.Model;
using PictureLibraryModel.Model.FileSystemModel;
using PictureLibraryModel.Model.LibraryModel;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using System;

namespace PictureLibraryModel.DataProviders.ImageProvider
{
    public class GoogleDriveImageFileProvider : IImageFileProvider
    {
        private readonly IGoogleDriveAPIClient _client;
        private readonly IFileService _fileService;
        private readonly Func<GoogleDriveImageFile> _imageFileLocator;

        public GoogleDriveRemoteStorageInfo RemoteStorageInfo { get; set; }

        public GoogleDriveImageFileProvider(
            IGoogleDriveAPIClient googleDriveAPIClient,
            IFileService fileService,
            Func<GoogleDriveImageFile> imageFileLocator)
        {
            _client = googleDriveAPIClient;
            _fileService = fileService;
            _imageFileLocator = imageFileLocator;
        }

        private GoogleDriveImageFile CreateImageFile(ImageFile imageFile, Google.Apis.Drive.v3.Data.File fileMetadata, GoogleDriveLibrary googleDriveLibrary)
        {
            GoogleDriveImageFile googleDriveImageFile = _imageFileLocator();
            googleDriveImageFile.FileId = fileMetadata.Id;
            googleDriveImageFile.LibraryFolderId = googleDriveLibrary.LibraryFolderId;
            googleDriveImageFile.ImagesFolderId = googleDriveImageFile.ImagesFolderId;
            googleDriveImageFile.LibraryFullName = googleDriveImageFile.Path;
            googleDriveImageFile.Extension = imageFile.Extension;
            googleDriveImageFile.Name = imageFile.Name;
            googleDriveImageFile.Path = string.Empty;
            googleDriveImageFile.RemoteStorageInfoId = RemoteStorageInfo.Id;
            googleDriveImageFile.Tags = imageFile.Tags;

            return googleDriveImageFile;
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, Library library)
        {
            if (library is not GoogleDriveLibrary googleDriveLibrary)
            {
                throw new InvalidOperationException("Invalid library type");
            }

            using (var stream = _fileService.OpenFile(imageFile.Path, System.IO.FileMode.Open))
            {
                var file = _client.UploadFileToFolder(stream, imageFile.Name, googleDriveLibrary.LibraryFolderId, $"image/{imageFile.Extension}", RemoteStorageInfo.UserName);
                return CreateImageFile(imageFile, file, googleDriveLibrary);
            }
        }

        public byte[] GetImageAsync(ImageFile imageFile)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile)
            {
                throw new InvalidOperationException("Invalid image file type");
            }

            using (var stream = _client.DownloadFile(googleDriveImageFile.FileId, RemoteStorageInfo.UserName))
            {
                return stream.ToArray();
            }
        }

        public void RemoveImage(ImageFile imageFile)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile)
            {
                throw new InvalidOperationException("Invalid image file type");
            }

            _client.RemoveFile(googleDriveImageFile.FileId, RemoteStorageInfo.UserName);
        }
    }
}
