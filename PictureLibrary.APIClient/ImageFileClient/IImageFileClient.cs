using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.APIClient.ImageFileClient
{
    public interface IImageFileClient
    {
        Task<IEnumerable<ApiImageFile>> GetAllImageFilesAsync(APIDataStoreInfo dataStoreInfo, Guid libraryId);
        Task<Stream> GetFileAsync(APIDataStoreInfo dataStoreInfo, Guid imageFileId);
        Task AddImageFileAsync(APIDataStoreInfo dataStoreInfo, ApiImageFile apiImageFile, Stream stream, IEnumerable<Guid> libraryIds);
        Task<bool> DeleteImageFileAsync(APIDataStoreInfo dataStoreInfo, Guid imageFileId);
        Task UpdateImageFileAsync(APIDataStoreInfo dataStoreInfo, ApiImageFile imageFile);
        Task UpdateFileContentAsync(APIDataStoreInfo dataStoreInfo, ApiImageFile imageFile, Stream contentStream);
        Task AddImageFileToLibraryAsync(APIDataStoreInfo dataStoreInfo, Guid imageFileId, Guid libraryId);
    }
}
