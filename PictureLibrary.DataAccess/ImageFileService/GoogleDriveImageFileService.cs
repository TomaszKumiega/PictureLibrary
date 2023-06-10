using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.Exceptions;
using PictureLibrary.GoogleDrive.MimeType;
using PictureLibrary.Tools.LibraryXml;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using File = Google.Apis.Drive.v3.Data.File;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public class GoogleDriveImageFileService
    {
        private static string _imagesFolderName => "Images";

        #region Private fields
        private readonly ILibraryXmlService<GoogleDriveLibrary> _libraryXmlService;
        private readonly IGoogleDriveApiClient _googleDriveApiClient;
        private readonly IDataStoreInfoService _dataStoreInfoProvider;
        #endregion

        public GoogleDriveImageFileService(
            ILibraryXmlService<GoogleDriveLibrary> libraryXmlService,
            IGoogleDriveApiClient googleDriveApiClient,
            IDataStoreInfoService dataStoreInfoProvider)
        {
            _libraryXmlService = libraryXmlService;
            _googleDriveApiClient = googleDriveApiClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        #region Public methods
        public async Task AddImageFileAsync(GoogleDriveImageFile imageFile, Stream imageFileContent, GoogleDriveLibrary library)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            string serializedLibrary = await GetLibraryFileContentAsync(library);

            var appFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, _googleDriveApiClient.GoogleDriveAppFolderName);

            if (appFolderId == null)
                return;

            var libraryFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, library.Id.ToString(), appFolderId);
            var imagesFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, _imagesFolderName, libraryFolderId);
            imagesFolderId ??= await _googleDriveApiClient.CreateFolderAsync(_imagesFolderName, dataStoreInfo.UserName, new List<string>() { libraryFolderId });

            string fileName = $"{imageFile.Name}.{imageFile.Extension}";
            var file = await _googleDriveApiClient.UploadFileToFolderAsync(imageFileContent, fileName, imagesFolderId, imageFile.GetMimeType(), dataStoreInfo.UserName);

            imageFile.FileId = file.Id;

            string updatedLibraryXml = _libraryXmlService.AddImageFileNode(serializedLibrary, imageFile);

            await WriteLibraryAsync(library, updatedLibraryXml);
        }

        public async Task<IEnumerable<GoogleDriveImageFile>> GetApiImageFilesAsync(GoogleDriveLibrary library)
        {
            string serializedLibrary = await GetLibraryFileContentAsync(library);

            return _libraryXmlService.GetImageFiles<GoogleDriveImageFile>(serializedLibrary);
        }

        public async Task<bool> DeleteImageFileAsync(GoogleDriveImageFile imageFile, GoogleDriveLibrary library)
        {
            string serializedLibrary = await GetLibraryFileContentAsync(library);
            string updatedLibraryXml = _libraryXmlService.RemoveImageFileNode(serializedLibrary, imageFile);
            return await WriteLibraryAsync(library, updatedLibraryXml);
        }

        public async Task UpdateImageFileAsync(GoogleDriveImageFile imageFile, GoogleDriveLibrary library)
        {
            string serializedLibrary = await GetLibraryFileContentAsync(library);
            string updatedLibraryXml = _libraryXmlService.UpdateImageFileNode(serializedLibrary, imageFile);
            _ = await WriteLibraryAsync(library, updatedLibraryXml);
        }

        public async Task<Stream> GetFileContentStreamAsync(GoogleDriveImageFile imageFile)
        {
            if (imageFile?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(imageFile));

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(imageFile.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();
            return await _googleDriveApiClient.DownloadFileAsync(imageFile.FileId, dataStoreInfo.UserName);
        }

        public async Task UpdateFileContentAsync(GoogleDriveImageFile imageFile, Stream updatedImageFileContentStream)
        {
            if (imageFile?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(imageFile));

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(imageFile.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            var file = new File()
            {
                Name = imageFile.Name
            };

            _ = await _googleDriveApiClient.UpdateFileAsync(file, updatedImageFileContentStream, imageFile.FileId, dataStoreInfo.UserName, imageFile.GetMimeType());
        }
        #endregion

        #region Private methods
        private async Task<string> GetLibraryFileContentAsync(GoogleDriveLibrary library)
        {
            if (library?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(library));

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            using var stream = await _googleDriveApiClient.DownloadFileAsync(library.FileId, dataStoreInfo.UserName);
            using var streamReader = new StreamReader(stream);

            return await streamReader.ReadToEndAsync();
        }

        private async Task<bool> WriteLibraryAsync(GoogleDriveLibrary library, string xml)
        {
            if (library?.FileId == null)
                throw new ArgumentException(string.Empty, nameof(library));

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            var file = new File()
            {
                Name = library.Name
            };

            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);

            await writer.WriteAsync(xml);

            string fileId = await _googleDriveApiClient.UpdateFileAsync(file, stream, library.FileId, dataStoreInfo.UserName, MimeTypes.Xml);
            
            return !string.IsNullOrEmpty(fileId);
        }
        #endregion
    }
}
