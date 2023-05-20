using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess
{
    public interface ILibrariesProvider
    {
        Task<IEnumerable<Library>> GetLibrariesFromAllSourcesAsync();
    }
}
