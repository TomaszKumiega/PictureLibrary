using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.APIClient.LibraryClient
{
    public interface ILibraryClient
    {
        Task<IEnumerable<ApiLibrary>> GetAllLibrariesAsync(APIDataStoreInfo dataStoreInfo);
        Task<ApiLibrary> AddLibraryAsync(APIDataStoreInfo dataStoreInfo, ApiLibrary library);
        Task UpdateLibraryAsync(APIDataStoreInfo dataStoreInfo, ApiLibrary library);
        Task<bool> DeleteLibraryAsync(APIDataStoreInfo dataStoreInfo, ApiLibrary library);
    }
}
