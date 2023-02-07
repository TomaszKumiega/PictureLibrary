using System.Collections.Generic;
using System.IO;

namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    public interface IGoogleDriveApiClient
    {
        MemoryStream DownloadFile(string fileId, string userName);
        Google.Apis.Drive.v3.Data.File UploadFileToFolder(Stream fileStream, string fileName, string folderId, string contentType, string userName);
        string CreateFolder(string folderName, string userName, List<string> parentsIds = null);
        IList<Google.Apis.Drive.v3.Data.File> SearchFiles(string userName, string filesType, string fields);
        string UpdateFile(Google.Apis.Drive.v3.Data.File updatedFileMedatada, Stream fileStream, string fileId, string userName, string contentType);
        bool FolderExists(string userName, string folderName, out string folderId);
        Google.Apis.Drive.v3.Data.File GetFileMetadata(string userName, string fileId, string fields);
        void RemoveFile(string fileId, string userName);
        bool Authorize(string userName);
    }
}
