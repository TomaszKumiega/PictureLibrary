using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.CredentialsProvider;
using PictureLibraryModel.Services.FileSystemServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    internal class GoogleDriveAPIClient : IGoogleDriveAPIClient
    {
        private readonly ICredentialsProvider _credentialsProvider;

        public GoogleDriveAPIClient(ICredentialsProvider credentialsProvider)
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

        public MemoryStream DownloadFile(string fileId, string userName)
        {
            DriveService service = GetDriveService(userName);

            FilesResource.GetRequest request = service.Files.Get(fileId);
            var stream = new MemoryStream();

            request.MediaDownloader.ProgressChanged += progress =>
            {
                if (progress.Exception != null)
                    throw progress.Exception;
            };

            request.Download(stream);

            return stream;
        }

        public Google.Apis.Drive.v3.Data.File UploadFileToFolder(Stream fileStream, string fileName, string folderId, string contentType, string userName)
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

            request.Upload();

            Google.Apis.Drive.v3.Data.File file = request.ResponseBody;

            return file;
        }
        
        public string CreateFolder(string folderName, string userName, List<string> parentsIds = null)
        {
            var service = GetDriveService(userName);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = GoogleDriveAPIMimeTypes.Folder,
                Parents = parentsIds,
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

        public string UpdateFile(Google.Apis.Drive.v3.Data.File updatedFileMedatada, Stream fileStream, string fileId, string userName, string contentType)
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

            updateRequest.Upload();
            
            var file = updateRequest.ResponseBody;
            
            return file.Id; 
        }

        public bool FolderExists(string userName, string folderName, out string folderId)
        {
            var files = SearchFiles(userName, GoogleDriveAPIMimeTypes.Folder, "files(id, name)");
            var file = files.FirstOrDefault(x => x.Name == folderName);

            if (file != null)
            {
                folderId = file.Id;
                return true;
            }
            else
            {
                folderId = null;
                return false;
            }
        }

        public Google.Apis.Drive.v3.Data.File GetFileMetadata(string userName, string fileId, string fields)
        {
            var service = GetDriveService(userName);

            FilesResource.GetRequest request = service.Files.Get(fileId);
            request.Fields = fields;

            var fileMetadata = request.Execute();

            return fileMetadata;
        }

        public void RemoveFile(string fileId, string userName)
        {
            DriveService service = GetDriveService(userName);

            FilesResource.DeleteRequest deleteRequest = service.Files.Delete(fileId);

            deleteRequest.Execute();
        }

        public bool Authorize(string userName)
        {
            return GetDriveService(userName) != null;
        }
    }
}
