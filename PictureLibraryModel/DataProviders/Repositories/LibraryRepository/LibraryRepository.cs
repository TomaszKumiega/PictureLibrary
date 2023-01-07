using PictureLibraryModel.DataProviders.Queries;
using PictureLibraryModel.Model;
using System;

namespace PictureLibraryModel.DataProviders.Repositories
{
    public class LibraryRepository : Repository<Library>, ILibraryRepository
    {
        private readonly Func<LibraryQueryBuilder> _queryBuilderLocator;

        public LibraryRepository(
            Func<LibraryQueryBuilder> queryBuilderLocator,
            IDataSourceCollection dataSourceCollection)
        {
            _queryBuilderLocator = queryBuilderLocator;
            _dataSourceCollection = dataSourceCollection;
        }

        public ILibraryQueryBuilder Query()
        {
            LibraryQueryBuilder queryBuilder = _queryBuilderLocator();
            queryBuilder.StartQuery(_dataSourceCollection);

            return queryBuilder;
        }

        public void AddLibrary(Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            dataSource.LibraryProvider.AddLibrary(library);
        }

        public void UpdateLibrary(Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            dataSource.LibraryProvider.UpdateLibrary(library);
        }

        public void RemoveLibrary(Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            dataSource.LibraryProvider.RemoveLibrary(library);
        }
    }
}
