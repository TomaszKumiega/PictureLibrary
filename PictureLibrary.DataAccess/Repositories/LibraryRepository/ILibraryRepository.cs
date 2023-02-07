using PictureLibrary.DataAccess.Queries.LibraryQuery;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Repositories.LibraryRepository
{
    public interface ILibraryRepository
    {
        void AddLibrary(Library library);
        void UpdateLibrary(Library library);
        void RemoveLibrary(Library library);
        ILibraryQueryBuilder Query();
    }
}