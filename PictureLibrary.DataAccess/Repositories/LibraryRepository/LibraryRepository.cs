using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Repositories.LibraryRepository
{
    public class LibraryRepository : Repository<Library>, ILibraryRepository
    {
        public LibraryRepository(
            IDataSourceCollection dataSourceCollection)
            : base(dataSourceCollection)
        {
        }

        public void AddLibrary(Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            dataSource.LibraryProvider!.AddLibrary(library);
        }

        public void UpdateLibrary(Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            dataSource.LibraryProvider!.UpdateLibrary(library);
        }

        public void RemoveLibrary(Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            dataSource.LibraryProvider!.RemoveLibrary(library);
        }
    }
}
