using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface IFileSystemLibraryService : ILibraryService
    {
        Task<IEnumerable<LocalLibrary>> GetAllLibrariesAsync();
    }
}