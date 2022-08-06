using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.CredentialsProvider;
using PictureLibraryModel.Services.FileSystemServices;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    internal class GoogleDriveAPIClient : IGoogleDriveAPIClient
    {
        private readonly ICredentialsProvider _credentialsProvider;
        private readonly IFileService _fileService;

        public GoogleDriveAPIClient(
            ICredentialsProvider credentialsProvider, 
            IFileService fileService)
        {
            _credentialsProvider = credentialsProvider;
            _fileService = fileService;
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

        public MemoryStream DownloadFile(string fileId, string userName)
        {
            DriveService service = GetDriveService(userName);

            FilesResource.GetRequest request = service.Files.Get(fileId);
            var stream = new MemoryStream();

            // TODO: request.MediaDownloader.ProgressChanged

            request.Download(stream);

            return stream;
        }

        public Google.Apis.Drive.v3.Data.File UploadFileToFolder(string filePath, string folderId, string contentType, string userName)
        {
            DriveService service = GetDriveService(userName);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = _fileService.GetInfo(filePath).Name,
                Parents = new List<string> { folderId },
            };

            FilesResource.CreateMediaUpload request = service.Files.Create(
                fileMetadata,
                _fileService.OpenFile(filePath, FileMode.Open),
                contentType);
            
            request.Fields = "id";
            
            request.Upload();

            Google.Apis.Drive.v3.Data.File file = request.ResponseBody;

            return file;
        }
        
        public string CreateFolder(string folderName, string userName)
        {
            var service = GetDriveService(userName);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnc.google-apps.folder"
            };

            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";

            var file = request.Execute();

            return file.Id;
        }

        public IList<Google.Apis.Drive.v3.Data.File> SearchFiles(string userName, string filesType, string fields)
        {
            var service = GetDriveService(userName);
            var files = new List<Google.Apis.Drive.v3.Data.File>();

            string pageToken = null;

            do
            {
                var request = service.Files.List();
                request.Q = $"mimeType='{filesType}'";
                request.Spaces = "drive";
                request.Fields = $"nextPageToken, {fields}";
                request.PageToken = pageToken;
                var result = request.Execute();

                files.AddRange(result.Files);

                pageToken = result.NextPageToken;

            } while (pageToken != null);

            return files;
        }
    }
}
