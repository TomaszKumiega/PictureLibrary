using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface ILibraryService
    {
        Task<Library> AddLibraryAsync(Library library);
        Task<bool> DeleteLibraryAsync(Library library);
        Task UpdateLibraryAsync(Library library);
    }
}
