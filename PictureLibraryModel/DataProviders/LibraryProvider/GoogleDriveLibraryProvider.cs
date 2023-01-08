﻿using PictureLibraryModel.Exceptions;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.LibraryModel;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Resources;
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
        private readonly IGoogleDriveApiClient _client;
        private readonly ILibraryFileService _libraryFileService;

        public GoogleDriveRemoteStorageInfo RemoteStorageInfo { get; set; }

        public GoogleDriveLibraryProvider(
            IGoogleDriveApiClient googleDriveAPIClient,
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

                var fileMetadata = _client.UploadFileToFolder(memoryStream, googleDriveLibrary.Name + Strings.LibraryFileExtension, googleDriveLibrary.LibraryFolderId, Strings.LibraryFileGoogleDriveContentType, RemoteStorageInfo.UserName);

                googleDriveLibrary.FileId = fileMetadata.Id;
            }
        }

        public IEnumerable<Library> GetAllLibraries()
        {
            return LoadLibraries();
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
            };

            using (var memoryStream = new MemoryStream())
            {
                _libraryFileService.WriteLibraryToStreamAsync(memoryStream, googleDriveLibrary, false);
                string fileId = _client.UpdateFile(fileMetadata, memoryStream, googleDriveLibrary.FileId, RemoteStorageInfo.UserName, Strings.LibraryFileGoogleDriveContentType);

                googleDriveLibrary.FileId = fileId;
            }
        }

        private string AddPictureLibraryFolder(string userName)
        {
            return _client.CreateFolder(Strings.PictureLibraryDirectory, userName);
        }

        private void AddLibraryFolder(GoogleDriveLibrary library, string userName, string pictureLibraryFolderId)
        {
            string libraryFolderId = _client.CreateFolder(library.Name, userName, new List<string> { pictureLibraryFolderId });
            string imagesFolderId = _client.CreateFolder(Strings.ImagesDirectory, userName, new List<string> { libraryFolderId });

            library.LibraryFolderId = libraryFolderId;
            library.ImagesFolderId = imagesFolderId;
        }

        public Library GetLibrary(string name)
        {
            var files = _client.SearchFiles(RemoteStorageInfo.UserName, Strings.LibraryFileGoogleDriveContentType, "files(id, parents, name)");
            var libraryFile = files.FirstOrDefault(x => x.Name.Contains(name));

            if (libraryFile != null)
            {
                var stream = _client.DownloadFile(libraryFile.Id, RemoteStorageInfo.UserName);
                var library = _libraryFileService.ReadLibraryFromStreamAsync<GoogleDriveLibrary>(stream);
                library.FileId = libraryFile.Id;

                stream.Close();

                return library;
            }

            return null;
        }

        public Library FindLibrary(Predicate<Library> predicate)
        {
            return LoadLibraries(predicate).LastOrDefault();
        }

        private List<Library> LoadLibraries(Predicate<Library> stopOnMatchingLibrary = null)
        {
            var libraries = new List<Library>();

            var files = _client.SearchFiles(RemoteStorageInfo.UserName, Strings.LibraryFileGoogleDriveContentType, "files(id, parents, name)");

            foreach (var fileId in files.Select(f => f.Id))
            {
                var stream = _client.DownloadFile(fileId, RemoteStorageInfo.UserName);
                var library = _libraryFileService.ReadLibraryFromStreamAsync<GoogleDriveLibrary>(stream);
                library.FileId = fileId;

                libraries.Add(library);

                stream.Close();

                if (stopOnMatchingLibrary != null && stopOnMatchingLibrary(library)) 
                {
                    return libraries;
                }
            }

            return libraries;
        }
    }
}
