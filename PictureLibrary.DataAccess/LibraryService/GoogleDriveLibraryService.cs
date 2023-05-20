﻿using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.Exceptions;
using PictureLibrary.GoogleDrive;
using PictureLibrary.GoogleDrive.MimeType;
using PictureLibrary.Tools.LibraryXml;
using PictureLibrary.Tools.XamlSerializer;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using File = Google.Apis.Drive.v3.Data.File;

namespace PictureLibrary.DataAccess.LibraryService
{
    public class GoogleDriveLibraryService : IGoogleDriveLibraryService
    {
        #region Private fields
        private static string AppFolder => "PictureLibraryAppFolder\\";

        private readonly IXmlSerializer _xmlSerializer;
        private readonly ILibraryXmlService _libraryXmlEditor;
        private readonly Func<IQueryBuilder> _queryBuilderLocator;
        private readonly IGoogleDriveApiClient _googleDriveApiClient;
        private readonly IDataStoreInfoService _dataStoreInfoProvider;
        #endregion

        public GoogleDriveLibraryService(
            IXmlSerializer xmlSerializer,
            ILibraryXmlService libraryXmlEditor,
            Func<IQueryBuilder> queryBuilderLocator,
            IGoogleDriveApiClient googleDriveApiClient,
            IDataStoreInfoService dataStoreInfoProvider)
        {
            _xmlSerializer = xmlSerializer;
            _libraryXmlEditor = libraryXmlEditor;
            _queryBuilderLocator = queryBuilderLocator;
            _googleDriveApiClient = googleDriveApiClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        #region Public methdods
        public async Task<IEnumerable<GoogleDriveLibrary>> GetAllLibrariesAsync(GoogleDriveDataStoreInfo dataStoreInfo)
        {
            var folderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, AppFolder);
            if (string.IsNullOrEmpty(folderId))
            {
                return Enumerable.Empty<GoogleDriveLibrary>();
            }

            var query = _queryBuilderLocator()
                .CreateQuery()
                .WithFileExtension(".plib")
                .GetQuery();

            var fields = "files(id)";

            var files = await _googleDriveApiClient.SearchFilesAsync(dataStoreInfo.UserName, query, fields);

            var serializedLibraries = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var memoryStream = await _googleDriveApiClient.DownloadFileAsync(file.Id, dataStoreInfo.UserName);
                using var streamReader = new StreamReader(memoryStream);

                serializedLibraries.Add(file.Id, streamReader.ReadToEnd());
            }

            return DeserializeLibraries(serializedLibraries);
        }

        public async Task<GoogleDriveLibrary> AddLibraryAsync(GoogleDriveLibrary library)
        {
            GoogleDriveDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            var folderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, AppFolder);
            if (string.IsNullOrEmpty(folderId))
            {
                folderId = await CreateFolderAsync(dataStoreInfo);
            }

            var libraryFolderId = await _googleDriveApiClient.CreateFolderAsync(library.Id.ToString(), dataStoreInfo.UserName, new List<string> { folderId });
            _ = await _googleDriveApiClient.CreateFolderAsync("Images", dataStoreInfo.UserName, new List<string>() { libraryFolderId });

            var stream = await SerializeLibraryAsync(library);

            string fileName = $"{library.Name}.plib";
            var file = await _googleDriveApiClient.UploadFileToFolderAsync(stream, fileName, libraryFolderId, MimeTypes.Xml, dataStoreInfo.UserName);

            library.FileId = file.Id;

            return library;
        }

        public async Task<bool> DeleteLibraryAsync(GoogleDriveLibrary library)
        {
            if (library?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(library));

            GoogleDriveDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            await _googleDriveApiClient.RemoveFileAsync(library.FileId, dataStoreInfo.UserName);

            return true;
        }

        public async Task UpdateLibraryAsync(GoogleDriveLibrary library)
        {
            if (library?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(library));

            GoogleDriveDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            var fileMetadata = new File()
            {
                Name = library.Name,
            };

            string libraryXml;
            using (MemoryStream stream = await _googleDriveApiClient.DownloadFileAsync(library.FileId, dataStoreInfo.UserName))
            using (StreamReader sr = new(stream))
            {
                libraryXml = await sr.ReadToEndAsync();
            }

            string updatedLibraryXml = _libraryXmlEditor.UpdateLibraryNode(libraryXml, library);

            using MemoryStream updateStream = new();
            using StreamWriter streamWriter = new(updateStream);

            await streamWriter.WriteAsync(updatedLibraryXml);

            await _googleDriveApiClient.UpdateFileAsync(fileMetadata, updateStream, library.FileId, dataStoreInfo.UserName, MimeTypes.Xml);
        }
        #endregion

        #region Private methods
        private async Task<string> CreateFolderAsync(GoogleDriveDataStoreInfo dataStoreInfo)
        {
            return await _googleDriveApiClient.CreateFolderAsync(AppFolder, dataStoreInfo.UserName);
        }

        private async Task<Stream> SerializeLibraryAsync(GoogleDriveLibrary library)
        {
            var serializedLibrary = _xmlSerializer.SerializeToString(library);

            var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteAsync(serializedLibrary);

            return stream;
        }

        private IEnumerable<GoogleDriveLibrary> DeserializeLibraries(Dictionary<string, string> serializedLibraries)
        {
            foreach (var serializedLibraryPair in serializedLibraries)
            {
                var library = _xmlSerializer.DeserializeFromString<GoogleDriveLibrary>(serializedLibraryPair.Value);

                if (library != null)
                {
                    library.FileId = serializedLibraryPair.Key;
                    yield return library;
                }
            }
        }
        #endregion
    }
}
