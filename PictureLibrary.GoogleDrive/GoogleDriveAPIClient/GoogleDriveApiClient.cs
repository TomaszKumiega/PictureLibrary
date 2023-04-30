using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using PictureLibraryModel.Services.CredentialsProvider;

namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    internal class GoogleDriveApiClient : IGoogleDriveApiClient
    {
        private readonly ICredentialsProvider _credentialsProvider;

        public GoogleDriveApiClient(ICredentialsProvider credentialsProvider)
        {
            _credentialsProvider = credentialsProvider;
        }

        #region Private methods
        private DriveService GetDriveService(string userName)
        {
            ClientSecrets clientSecrets = _credentialsProvider.GetGoogleDriveAPIClientSecrets();
            string[] scopes = new[] { DriveService.ScopeConstants.Drive };
            var fileDataStore = new FileDataStore("UserCredential");

            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets: clientSecrets,
                scopes: scopes,
                user: userName,
                taskCancellationToken: CancellationToken.None,
                dataStore: fileDataStore).GetAwaiter().GetResult();

            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Picture library"
            });
        }
        #endregion

        public async Task<MemoryStream> DownloadFileAsync(string fileId, string userName)
        {
            DriveService service = GetDriveService(userName);

            FilesResource.GetRequest request = service.Files.Get(fileId);
            var stream = new MemoryStream();

            request.MediaDownloader.ProgressChanged += progress =>
            {
                if (progress.Exception != null)
                    throw progress.Exception;
            };

            await request.DownloadAsync(stream);

            return stream;
        }

        public async Task<Google.Apis.Drive.v3.Data.File> UploadFileToFolderAsync(Stream fileStream, string fileName, string folderId, string contentType, string userName)
        {
            DriveService service = GetDriveService(userName);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { folderId },
            };

            FilesResource.CreateMediaUpload request = service.Files.Create(
                fileMetadata,
                fileStream,
                contentType);
            
            request.Fields = "id";
            
            request.ProgressChanged += progress =>
            {
                if (progress.Exception != null)
                    throw progress.Exception;
            };

            await request.UploadAsync();

            Google.Apis.Drive.v3.Data.File file = request.ResponseBody;

            return file;
        }
        
        public async Task<string> CreateFolderAsync(string folderName, string userName, List<string>? parentsIds = null)
        {
            var service = GetDriveService(userName);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = GoogleDriveApiMimeTypes.Folder,
                Parents = parentsIds,
            };

            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";

            var file = await request.ExecuteAsync();

            return file.Id;
        }

        public async Task<IList<Google.Apis.Drive.v3.Data.File>> SearchFilesAsync(string userName, string query, string fields)
        {
            var service = GetDriveService(userName);
            var files = new List<Google.Apis.Drive.v3.Data.File>();

            string? pageToken = null;

            do
            {
                var request = service.Files.List();
                request.Q = query;
                request.Spaces = "drive";
                request.Fields = $"nextPageToken, {fields}";
                request.PageToken = pageToken;
                var result = await request.ExecuteAsync();

                files.AddRange(result.Files);

                pageToken = result.NextPageToken;

            } while (pageToken != null);

            return files;
        }

        public async Task<string> UpdateFileAsync(Google.Apis.Drive.v3.Data.File updatedFileMedatada, Stream fileStream, string fileId, string userName, string contentType)
        {
            var service = GetDriveService(userName);

            FilesResource.UpdateMediaUpload updateRequest;
            updateRequest = service.Files.Update(
                updatedFileMedatada,
                fileId,
                fileStream,
                contentType);
            updateRequest.Fields = "id";

            updateRequest.ProgressChanged += progress =>
            {
                if (progress.Exception != null)
                    throw progress.Exception;
            };

            await updateRequest.UploadAsync();
            
            var file = updateRequest.ResponseBody;
            
            return file.Id; 
        }

        public async Task<string> FolderExistsAsync(string userName, string folderName)
        {
            var files = await SearchFilesAsync(userName, GoogleDriveApiMimeTypes.Folder, "files(id, name)");
            var file = files.FirstOrDefault(x => x.Name == folderName);

            if (file != null)
            {
                return file.Id;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<Google.Apis.Drive.v3.Data.File> GetFileMetadataAsync(string userName, string fileId, string fields)
        {
            var service = GetDriveService(userName);

            FilesResource.GetRequest request = service.Files.Get(fileId);
            request.Fields = fields;

            var fileMetadata = await request.ExecuteAsync();

            return fileMetadata;
        }

        public async Task RemoveFileAsync(string fileId, string userName)
        {
            DriveService service = GetDriveService(userName);

            FilesResource.DeleteRequest deleteRequest = service.Files.Delete(fileId);

            await deleteRequest.ExecuteAsync();
        }

        public bool Authorize(string userName)
        {
            return GetDriveService(userName) != null;
        }
    }
}
