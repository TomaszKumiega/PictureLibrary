using PictureLibraryModel.Exceptions;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.LibraryModel;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PictureLibraryModel.DataProviders.LibraryProvider
{
    public class GoogleDriveLibraryProvider : ILibraryProvider
    {
        private readonly IGoogleDriveAPIClient _client;
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILibraryFileService _libraryFileService;

        public GoogleDriveRemoteStorageInfo RemoteStorageInfo { get; set; }

        public GoogleDriveLibraryProvider(
            IGoogleDriveAPIClient googleDriveAPIClient,
            ISettingsProvider settingsProvider,
            ILibraryFileService libraryFileService)
        {
            _client = googleDriveAPIClient;
            _settingsProvider = settingsProvider;
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

            GoogleDriveRemoteStorageInfo remoteStorageInfo = _settingsProvider.Settings.RemoteStorageInfos.FirstOrDefault(x => x.Id == googleDriveLibrary.RemoteStorageInfoId) as GoogleDriveRemoteStorageInfo;
            if (remoteStorageInfo == null)
            {
                throw new InvalidOperationException("No remote storage info found in the library object");
            }

            if (!_client.FolderExists(remoteStorageInfo.UserName, "PictureLibrary", out string pictureLibraryFolderId))
            {
                AddPictureLibraryFolder(remoteStorageInfo.UserName);
            }

            if (_client.FolderExists(remoteStorageInfo.UserName, googleDriveLibrary.Name, out var _))
            {
                throw new LibraryAlreadyExistsException(googleDriveLibrary.Name);
            }

            AddLibraryFolder(googleDriveLibrary, remoteStorageInfo.UserName, pictureLibraryFolderId);

            using (var memoryStream = new MemoryStream())
            {
                _libraryFileService.WriteLibraryToStreamAsync(memoryStream, googleDriveLibrary);

                var fileMetadata = _client.UploadFileToFolder(memoryStream, googleDriveLibrary.Name + ".plib", googleDriveLibrary.LibraryFolderId, "xml/plib", remoteStorageInfo.UserName);

                googleDriveLibrary.FileId = fileMetadata.Id;
            }
        }

        public IEnumerable<Library> GetAllLibraries()
        {
            var libraries = new List<Library>();

            var files = _client.SearchFiles(RemoteStorageInfo.UserName, GoogleDriveAPIMimeTypes.File, "files(id, parents)");
            var libraryFiles = files.Where(x => x.Parents.Contains("PictureLibrary")).ToList();
            
            foreach (var file in libraryFiles)
            {
                var stream = _client.DownloadFile(file.Id, RemoteStorageInfo.UserName);
                var library = _libraryFileService.ReadLibraryFromStreamAsync<GoogleDriveLibrary>(stream);
                libraries.Add(library);
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
                _libraryFileService.WriteLibraryToStreamAsync(memoryStream, googleDriveLibrary);
                string fileId = _client.UpdateFile(fileMetadata, memoryStream, googleDriveLibrary.FileId, RemoteStorageInfo.UserName, "xml/plib");

                googleDriveLibrary.FileId = fileId;
            }
        }

        private void AddPictureLibraryFolder(string userName)
        {
            _ = _client.CreateFolder("PictureLibrary", userName);
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
