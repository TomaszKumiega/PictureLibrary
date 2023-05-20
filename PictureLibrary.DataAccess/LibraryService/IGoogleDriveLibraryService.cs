using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface IGoogleDriveLibraryService
    {
        Task<GoogleDriveLibrary> AddLibraryAsync(GoogleDriveLibrary library);
        Task<bool> DeleteLibraryAsync(GoogleDriveLibrary library);
        Task<IEnumerable<GoogleDriveLibrary>> GetAllLibrariesAsync(GoogleDriveDataStoreInfo dataStoreInfo);
        Task UpdateLibraryAsync(GoogleDriveLibrary library);
    }
}