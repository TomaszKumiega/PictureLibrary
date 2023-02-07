using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Queries.LibraryQuery
{
    public class LibraryQueryBuilder : QueryBuilder<ILibraryQueryBuilder, Library>, ILibraryQueryBuilder
    {
        public LibraryQueryBuilder(Func<StorageQuery> storageQuery) : base(storageQuery)
        {
        }

        public ILibraryQueryBuilder StartQuery(IDataSourceCollection dataSourceCollection)
        {
            _dataSourceCollection = dataSourceCollection;

            return this;
        }

        public override List<Library> ToList()
        {
            EnsureQueryWasStarted();

            List<Library> libraries = new();

            if (_query?.DataSources?.Any() != true)
                throw new InvalidOperationException("Data sources should be specified before running the query");

            foreach (var remoteStorageId in _query.DataSources)
            {
                IDataSource dataSource = _dataSourceCollection!.GetDataSourceByRemoteStorageId(remoteStorageId);

                if (_query.GetAllData)
                {
                    libraries.AddRange(dataSource.LibraryProvider!.GetAllLibraries().ToList());
                }
                else if (_query.Id.HasValue)
                {
                    var library = dataSource.LibraryProvider!.FindLibrary(x => x.Id == _query.Id);
                    
                    if (library != null) 
                        libraries.Add(library);
                }
            }

            return libraries;
        }
    }
}
