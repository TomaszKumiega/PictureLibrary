namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    public interface IGoogleDriveApiClient
    {
        Task<MemoryStream> DownloadFileAsync(string fileId, string userName);
        Task<Google.Apis.Drive.v3.Data.File> UploadFileToFolderAsync(Stream fileStream, string fileName, string folderId, string contentType, string userName);
        Task<string> CreateFolderAsync(string folderName, string userName, List<string>? parentsIds = null);
        Task<IList<Google.Apis.Drive.v3.Data.File>> SearchFilesAsync(string userName, string query, string fields);
        Task<string> UpdateFileAsync(Google.Apis.Drive.v3.Data.File updatedFileMedatada, Stream fileStream, string fileId, string userName, string contentType);
        Task<string> FolderExistsAsync(string userName, string folderName);
        Task<Google.Apis.Drive.v3.Data.File> GetFileMetadataAsync(string userName, string fileId, string fields);
        Task RemoveFileAsync(string fileId, string userName);
        bool Authorize(string userName);
    }
}
