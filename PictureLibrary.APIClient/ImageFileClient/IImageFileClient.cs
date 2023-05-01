using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.APIClient.ImageFileClient
{
    public interface IImageFileClient
    {
        Task<IEnumerable<ApiImageFile>> GetAllImageFilesAsync(APIDataStoreInfo dataStoreInfo, Guid libraryId);
        Task<Stream> GetFileAsync(APIDataStoreInfo dataStoreInfo, Guid imageFileId);
        Task UploadFileAsync(APIDataStoreInfo dataStoreInfo, Stream stream, string fileName, IEnumerable<Guid> libraryIds);
        Task<bool> DeleteImageFileAsync(APIDataStoreInfo dataStoreInfo, Guid imageFileId);
        Task UpdateImageFileAsync(APIDataStoreInfo dataStoreInfo, ApiImageFile imageFile);
        Task UpdateFileContentAsync(APIDataStoreInfo dataStoreInfo, ApiImageFile imageFile, Stream contentStream);
    }
}
