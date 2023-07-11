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
    public class GoogleDriveImageFileService : IImageFileService
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
        public async Task AddImageFile(ImageFile imageFile, Stream imageFileContent, Library library)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile)
                throw new ArgumentException("Invalid image file type.", nameof(imageFile));

            if (library is not GoogleDriveLibrary googleDriveLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(googleDriveLibrary.DataStoreInfoId) 
                ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            string serializedLibrary = await GetLibraryFileContentAsync(googleDriveLibrary);

            var appFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, _googleDriveApiClient.GoogleDriveAppFolderName);

            if (appFolderId == null)
                return;

            var libraryFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, library.Id.ToString(), appFolderId);
            var imagesFolderId = await _googleDriveApiClient.GetFolderIdAsync(dataStoreInfo.UserName, _imagesFolderName, libraryFolderId);
            imagesFolderId ??= await _googleDriveApiClient.CreateFolderAsync(_imagesFolderName, dataStoreInfo.UserName, new List<string>() { libraryFolderId });

            string fileName = $"{imageFile.Name}.{imageFile.Extension}";
            var file = await _googleDriveApiClient.UploadFileToFolderAsync(imageFileContent, fileName, imagesFolderId, googleDriveImageFile.GetMimeType(), dataStoreInfo.UserName);

            googleDriveImageFile.FileId = file.Id;

            string updatedLibraryXml = _libraryXmlService.AddImageFileNode(serializedLibrary, googleDriveImageFile);

            await WriteLibraryAsync(googleDriveLibrary, updatedLibraryXml);
        }

        public async Task<IEnumerable<ImageFile>> GetAllImageFiles(Library library)
        {
            if (library is not GoogleDriveLibrary googleDriveLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            string serializedLibrary = await GetLibraryFileContentAsync(googleDriveLibrary);

            return _libraryXmlService.GetImageFiles<GoogleDriveImageFile>(serializedLibrary);
        }

        public async Task DeleteImageFile(ImageFile imageFile, Library library)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile)
                throw new ArgumentException("Invalid image file type.", nameof(imageFile));

            if (library is not GoogleDriveLibrary googleDriveLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            string serializedLibrary = await GetLibraryFileContentAsync(googleDriveLibrary);
            string updatedLibraryXml = _libraryXmlService.RemoveImageFileNode(serializedLibrary, googleDriveImageFile);
            await WriteLibraryAsync(googleDriveLibrary, updatedLibraryXml);
        }

        public async Task UpdateImageFile(ImageFile imageFile, Library library)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile)
                throw new ArgumentException("Invalid image file type.", nameof(imageFile));

            if (library is not GoogleDriveLibrary googleDriveLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            string serializedLibrary = await GetLibraryFileContentAsync(googleDriveLibrary);
            string updatedLibraryXml = _libraryXmlService.UpdateImageFileNode(serializedLibrary, googleDriveImageFile);
            _ = await WriteLibraryAsync(googleDriveLibrary, updatedLibraryXml);
        }

        public async Task<Stream> GetFileContent(ImageFile imageFile)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile
                || googleDriveImageFile?.FileId == null)
            {
                throw new ArgumentException("Invalid image file type.", nameof(imageFile));
            }

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(googleDriveImageFile.DataStoreInfoId) 
                ?? throw new GoogleDriveAccountConfigurationNotFoundException();
            return await _googleDriveApiClient.DownloadFileAsync(googleDriveImageFile.FileId, dataStoreInfo.UserName);
        }

        public async Task UpdateFileContent(ImageFile imageFile, Stream updatedImageFileContentStream)
        {
            if (imageFile is not GoogleDriveImageFile googleDriveImageFile
                || googleDriveImageFile?.FileId == null)
            {
                throw new ArgumentException("Invalid image file.", nameof(imageFile));
            }

            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(googleDriveImageFile.DataStoreInfoId) 
                ?? throw new GoogleDriveAccountConfigurationNotFoundException();

            var file = new File()
            {
                Name = googleDriveImageFile.Name
            };

            _ = await _googleDriveApiClient.UpdateFileAsync(file, updatedImageFileContentStream, googleDriveImageFile.FileId, dataStoreInfo.UserName, googleDriveImageFile.GetMimeType());
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
