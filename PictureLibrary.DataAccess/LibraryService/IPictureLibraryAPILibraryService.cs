using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface IPictureLibraryAPILibraryService
    {
        Task<ApiLibrary> AddLibraryAsync(ApiLibrary library);
        Task<bool> DeleteLibraryAsync(ApiLibrary library);
        Task<IEnumerable<ApiLibrary>> GetAllLibrariesAsync(APIDataStoreInfo dataStoreInfo);
        Task UpdateLibraryAsync(ApiLibrary library);
    }
}