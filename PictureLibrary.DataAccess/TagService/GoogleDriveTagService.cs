﻿using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.Exceptions;
using PictureLibrary.GoogleDrive.MimeType;
using PictureLibrary.Tools.LibraryXml;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using File = Google.Apis.Drive.v3.Data.File;

namespace PictureLibrary.DataAccess.TagService
{
    public class GoogleDriveTagService
    {
        #region Private fields
        private readonly ILibraryXmlService _libraryXmlService;
        private readonly IGoogleDriveApiClient _googleDriveApiClient;
        private readonly IDataStoreInfoService _dataStoreInfoProvider;
        #endregion

        public GoogleDriveTagService(
            ILibraryXmlService libraryXmlService,
            IGoogleDriveApiClient googleDriveApiClient,
            IDataStoreInfoService dataStoreInfoProvider)
        {
            _libraryXmlService = libraryXmlService;
            _googleDriveApiClient = googleDriveApiClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        #region Public methods
        public async Task AddTagAsync(GoogleDriveLibrary library, Tag tag)
        {
            var serializedLibrary = await GetLibraryXmlAsync(library);
            var updatedLibraryXml = _libraryXmlService.AddTagNode(serializedLibrary, tag);
            await WriteLibraryXmlAsync(updatedLibraryXml, library);
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync(GoogleDriveLibrary library)
        {
            var serializedLibrary = await GetLibraryXmlAsync(library);
            return _libraryXmlService.GetTags(serializedLibrary);
        }

        public async Task<bool> DeleteTagAsync(GoogleDriveLibrary library, Tag tag)
        {
            var serializedLibrary = await GetLibraryXmlAsync(library);
            var updatedLibraryXml = _libraryXmlService.RemoveTagNode(serializedLibrary, tag);
            return await WriteLibraryXmlAsync(updatedLibraryXml, library);
        }

        public async Task<bool> UpdateTagAsync(GoogleDriveLibrary library, Tag tag)
        {
            var serializedLibrary = await GetLibraryXmlAsync(library);
            var updatedLibraryXml = _libraryXmlService.UpdateTagNode(serializedLibrary, tag);
            return await WriteLibraryXmlAsync(updatedLibraryXml, library);
        }
        #endregion

        #region Private methods
        private async Task<string> GetLibraryXmlAsync(GoogleDriveLibrary library)
        {
            if (library?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(Library));

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();
            using var stream = await _googleDriveApiClient.DownloadFileAsync(library.FileId, dataStoreInfo.UserName);
            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        private async Task<bool> WriteLibraryXmlAsync(string xml, GoogleDriveLibrary library)
        {
            if (library?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(library));

            var file = new File()
            {
                Name = $"{library.Name}.plib",
            };

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(xml);

            var fileId = await _googleDriveApiClient.UpdateFileAsync(file, stream, library.FileId, dataStoreInfo.UserName, MimeTypes.Xml);

            return !string.IsNullOrEmpty(fileId);
        }
        #endregion
    }
}
