using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.GoogleDrive.MimeType;
using PictureLibrary.Tools.XamlEditor;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using File = Google.Apis.Drive.v3.Data.File;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public class GoogleDriveImageFileService
    {
        private readonly ILibraryXmlService _libraryXmlService;
        private readonly IGoogleDriveApiClient _googleDriveApiClient;
        private readonly IDataStoreInfoService _dataStoreInfoProvider;

        public GoogleDriveImageFileService(
            ILibraryXmlService libraryXmlService,
            IGoogleDriveApiClient googleDriveApiClient,
            IDataStoreInfoService dataStoreInfoProvider)
        {
            _libraryXmlService = libraryXmlService;
            _googleDriveApiClient = googleDriveApiClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        public async Task AddImageFileAsync(GoogleDriveImageFile imageFile, Stream imageFileContent, GoogleDriveLibrary library)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId);

            string serializedLibrary = await GetLibraryFileContentAsync(library);

            var appFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, _googleDriveApiClient.AppFolder);

            if (appFolderId == null)
                return;

            var libraryFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, library.Id.ToString(), appFolderId);
            var imagesFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, "Images", libraryFolderId);
            imagesFolderId ??= await _googleDriveApiClient.CreateFolderAsync("Images", dataStoreInfo.UserName, new List<string>() { libraryFolderId });

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
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(imageFile.DataStoreInfoId);
            return await _googleDriveApiClient.DownloadFileAsync(imageFile.FileId, dataStoreInfo.UserName);
        }

        public async Task UpdateFileContentAsync(GoogleDriveImageFile imageFile, Stream updatedImageFileContentStream)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(imageFile.DataStoreInfoId);

            var file = new File()
            {
                Name = imageFile.Name
            };

            _ = await _googleDriveApiClient.UpdateFileAsync(file, updatedImageFileContentStream, imageFile.FileId, dataStoreInfo.UserName, imageFile.GetMimeType());
        }

        private async Task<string> GetLibraryFileContentAsync(GoogleDriveLibrary library)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId);

            using var stream = await _googleDriveApiClient.DownloadFileAsync(library.FileId, dataStoreInfo.UserName);
            using var streamReader = new StreamReader(stream);

            return await streamReader.ReadToEndAsync();
        }

        private async Task<bool> WriteLibraryAsync(GoogleDriveLibrary library, string xml)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId);

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
    }
}
