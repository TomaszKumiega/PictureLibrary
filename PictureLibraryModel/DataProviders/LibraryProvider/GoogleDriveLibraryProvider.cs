using PictureLibraryModel.Exceptions;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.LibraryModel;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.LibraryFileService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PictureLibraryModel.DataProviders.LibraryProvider
{
    public class GoogleDriveLibraryProvider : ILibraryProvider
    {
        private readonly IGoogleDriveAPIClient _client;
        private readonly ILibraryFileService _libraryFileService;

        public GoogleDriveRemoteStorageInfo RemoteStorageInfo { get; set; }

        public GoogleDriveLibraryProvider(
            IGoogleDriveAPIClient googleDriveAPIClient,
            ILibraryFileService libraryFileService)
        {
            _client = googleDriveAPIClient;
            _libraryFileService = libraryFileService;
        }

        public void AddLibraries(IEnumerable<Library> libraries)
        {
            foreach (Library library in libraries)
            {
                AddLibrary(library);
            }
        }

        public void AddLibrary(Library library)
        {
            if (library is not GoogleDriveLibrary googleDriveLibrary)
            {
                throw new InvalidOperationException("Invalid library type");
            }

            if (!_client.FolderExists(RemoteStorageInfo.UserName, "PictureLibrary", out string pictureLibraryFolderId))
            {
                pictureLibraryFolderId = AddPictureLibraryFolder(RemoteStorageInfo.UserName);
            }

            if (_client.FolderExists(RemoteStorageInfo.UserName, googleDriveLibrary.Name, out var _))
            {
                throw new LibraryAlreadyExistsException(googleDriveLibrary.Name);
            }

            AddLibraryFolder(googleDriveLibrary, RemoteStorageInfo.UserName, pictureLibraryFolderId);

            using (var memoryStream = new MemoryStream())
            {
                _libraryFileService.WriteLibraryToStreamAsync(memoryStream, googleDriveLibrary, false);

                var fileMetadata = _client.UploadFileToFolder(memoryStream, googleDriveLibrary.Name + ".plib", googleDriveLibrary.LibraryFolderId, "xml/plib", RemoteStorageInfo.UserName);

                googleDriveLibrary.FileId = fileMetadata.Id;
            }
        }

        public IEnumerable<Library> GetAllLibraries()
        {
            var libraries = new List<Library>();

            var files = _client.SearchFiles(RemoteStorageInfo.UserName, "xml/plib", "files(id, parents, name)");
            
            foreach (var file in files)
            {
                var stream = _client.DownloadFile(file.Id, RemoteStorageInfo.UserName);
                var library = _libraryFileService.ReadLibraryFromStreamAsync<GoogleDriveLibrary>(stream);
                library.FileId = file.Id;

                libraries.Add(library);

                stream.Close();
            }

            return libraries;
        }

        public void RemoveLibrary(Library library)
        {
            if (library is not GoogleDriveLibrary googleDriveLibrary)
            {
                throw new InvalidOperationException("Invalid library type");
            }

            _client.RemoveFile(googleDriveLibrary.FileId, RemoteStorageInfo.UserName);
        }

        public void UpdateLibrary(Library library)
        {
            if (library is not GoogleDriveLibrary googleDriveLibrary)
            {
                throw new InvalidOperationException("Invalid library type");
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = googleDriveLibrary.Name,
                Parents = new List<string> { googleDriveLibrary.LibraryFolderId }
            };

            using (var memoryStream = new MemoryStream())
            {
                _libraryFileService.WriteLibraryToStreamAsync(memoryStream, googleDriveLibrary, false);
                string fileId = _client.UpdateFile(fileMetadata, memoryStream, googleDriveLibrary.FileId, RemoteStorageInfo.UserName, "xml/plib");

                googleDriveLibrary.FileId = fileId;
            }
        }

        private string AddPictureLibraryFolder(string userName)
        {
            return _client.CreateFolder("PictureLibrary", userName);
        }

        private void AddLibraryFolder(GoogleDriveLibrary library, string userName, string pictureLibraryFolderId)
        {
            string libraryFolderId = _client.CreateFolder(library.Name, userName, new List<string> { pictureLibraryFolderId });
            string imagesFolderId = _client.CreateFolder("Images", userName, new List<string> { libraryFolderId });

            library.LibraryFolderId = libraryFolderId;
            library.ImagesFolderId = imagesFolderId;
        }
    }
}
