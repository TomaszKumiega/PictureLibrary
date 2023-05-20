using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface IFileSystemLibraryService
    {
        Task<LocalLibrary> AddLibraryAsync(LocalLibrary library);
        Task<bool> DeleteLibraryAsync(LocalLibrary library);
        Task<IEnumerable<LocalLibrary>> GetAllLibrariesAsync();
        Task UpdateLibraryAsync(LocalLibrary library);
    }
}