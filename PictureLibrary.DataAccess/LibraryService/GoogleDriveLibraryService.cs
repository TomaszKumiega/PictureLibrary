using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.GoogleDrive.MimeType;
using PictureLibrary.GoogleDrive.QueryBuilder;
using PictureLibrary.Tools.XamlEditor;
using PictureLibrary.Tools.XamlSerializer;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using File = Google.Apis.Drive.v3.Data.File;

namespace PictureLibrary.DataAccess.LibraryService
{
    public class GoogleDriveLibraryService : ILibraryService<GoogleDriveLibrary>
    {
        private static string AppFolder => "PictureLibraryAppFolder\\";

        private readonly ILibraryXmlService _libraryXmlEditor;
        private readonly Func<IQueryBuilder> _queryBuilderLocator;
        private readonly IGoogleDriveApiClient _googleDriveApiClient;
        private readonly IDataStoreInfoProvider _dataStoreInfoProvider;
        private readonly IXmlSerializer<GoogleDriveLibrary> _xmlSerializer;

        public GoogleDriveLibraryService(
            ILibraryXmlService libraryXmlEditor,
            Func<IQueryBuilder> queryBuilderLocator,
            IGoogleDriveApiClient googleDriveApiClient,
            IDataStoreInfoProvider dataStoreInfoProvider,
            IXmlSerializer<GoogleDriveLibrary> xmlSerializer)
        {
            _xmlSerializer = xmlSerializer;
            _libraryXmlEditor = libraryXmlEditor;
            _queryBuilderLocator = queryBuilderLocator;
            _googleDriveApiClient = googleDriveApiClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        #region GetAll
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

        private IEnumerable<GoogleDriveLibrary> DeserializeLibraries(Dictionary<string, string> serializedLibraries)
        {
            foreach (var serializedLibraryPair in serializedLibraries)
            {
                var library = _xmlSerializer.DeserializeFromString(serializedLibraryPair.Value);

                if (library != null)
                {
                    library.FileId = serializedLibraryPair.Key;
                    yield return library;
                }
            }
        }
        #endregion

        public async Task<GoogleDriveLibrary> AddLibraryAsync(GoogleDriveLibrary library)
        {
            GoogleDriveDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId);
            
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
            GoogleDriveDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId);

            await _googleDriveApiClient.RemoveFileAsync(library.FileId, dataStoreInfo.UserName);

            return true;
        }

        public async Task UpdateLibraryAsync(GoogleDriveLibrary library)
        {
            GoogleDriveDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<GoogleDriveDataStoreInfo>(library.DataStoreInfoId);

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
    }
}
