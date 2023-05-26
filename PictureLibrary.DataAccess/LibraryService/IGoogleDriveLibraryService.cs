using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface IGoogleDriveLibraryService : ILibraryService
    {
        Task<IEnumerable<GoogleDriveLibrary>> GetAllLibrariesAsync(GoogleDriveDataStoreInfo dataStoreInfo);
    }
}