using File = Google.Apis.Drive.v3.Data.File;

namespace PictureLibraryModel.Services.GoogleDriveAPIClient
{
    public interface IGoogleDriveApiClient
    {
        string AppFolder { get; }
        Task<MemoryStream> DownloadFileAsync(string fileId, string userName);
        Task<File> UploadFileToFolderAsync(Stream fileStream, string fileName, string folderId, string contentType, string userName);
        Task<string> CreateFolderAsync(string folderName, string userName, List<string>? parentsIds = null);
        Task<IList<File>> SearchFilesAsync(string userName, string query, string fields);
        Task<string> UpdateFileAsync(File updatedFileMedatada, Stream fileStream, string fileId, string userName, string contentType);
        Task<string> GetFolderIdAsync(string userName, string folderName, string? parentId = null);
        Task<File> GetFileMetadataAsync(string userName, string fileId, string fields);
        Task RemoveFileAsync(string fileId, string userName);
        bool Authorize(string userName);
    }
}
