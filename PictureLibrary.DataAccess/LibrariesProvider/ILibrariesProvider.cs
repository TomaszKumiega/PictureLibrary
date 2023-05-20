using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.LibrariesProvider
{
    public interface ILibrariesProvider
    {
        Task<IEnumerable<Library>> GetLibrariesFromAllSourcesAsync();
    }
}
