using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess
{
    public interface ILibrariesProvider
    {
        Task<IEnumerable<Library>> GetLibrariesFromAllSourcesAsync();
        void AddLibraryToCache(Library library);
        Library? GetLibraryFromCacheById(Guid id, bool removeFromCache = false);
    }
}
