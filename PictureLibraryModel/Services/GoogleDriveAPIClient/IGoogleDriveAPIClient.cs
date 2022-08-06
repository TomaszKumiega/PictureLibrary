using System.Collections.Generic;
using System.IO;

namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    public interface IGoogleDriveAPIClient
    {
        MemoryStream DownloadFile(string fileId, string userName);
        Google.Apis.Drive.v3.Data.File UploadFileToFolder(string filePath, string folderId, string contentType, string userName);
        string CreateFolder(string folderName, string userName);
        IList<Google.Apis.Drive.v3.Data.File> SearchFiles(string userName, string filesType, string fields);
    }
}
