using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.LibraryService
{
    public interface ILibraryService<TLibrary>
        where TLibrary : Library
    {
        Task<TLibrary> AddLibraryAsync(TLibrary library);
        Task UpdateLibraryAsync(TLibrary library);
        Task<bool> DeleteLibraryAsync(TLibrary library);
    }
}
