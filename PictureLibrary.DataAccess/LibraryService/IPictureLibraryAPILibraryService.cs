using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface IPictureLibraryAPILibraryService : ILibraryService
    {
        Task<IEnumerable<ApiLibrary>> GetAllLibrariesAsync(ApiDataStoreInfo dataStoreInfo);
    }
}