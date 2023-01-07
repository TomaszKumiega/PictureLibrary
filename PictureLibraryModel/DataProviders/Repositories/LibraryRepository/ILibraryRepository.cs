using PictureLibraryModel.DataProviders.Queries;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders.Repositories
{
    public interface ILibraryRepository
    {
        void AddLibrary(Library library);
        void UpdateLibrary(Library library);
        void RemoveLibrary(Library library);
        ILibraryQueryBuilder Query();
    }
}