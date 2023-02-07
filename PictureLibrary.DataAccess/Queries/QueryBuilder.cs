using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Exceptions;

namespace PictureLibrary.DataAccess.Queries
{
    public abstract class QueryBuilder<TBuilderInterface, TEntity> : IQueryBuilder<TBuilderInterface, TEntity>
        where TBuilderInterface : class
        where TEntity : class
    {
        protected readonly StorageQuery _query;
        protected IDataSourceCollection? _dataSourceCollection;

        protected QueryBuilder(Func<StorageQuery> storageQueryLocator)
        {
            _query = storageQueryLocator();
        }

        protected void EnsureQueryWasStarted()
        {
            if (_dataSourceCollection == null)
                throw new InvalidOperationException("Query was not started");
        }

        public TBuilderInterface FromDataSources(params Guid?[] sources)
        {
            EnsureQueryWasStarted();

            foreach (var sourceId in sources)
            {
                if (!_dataSourceCollection!.DataSources.Any(x => x.RemoteStorageInfo!.Id == sourceId))
                    throw new ArgumentException($"There is no data source with id {sourceId}");
            }

            _query.DataSources = sources.ToList();

            return this is TBuilderInterface builderInterface
                ? builderInterface
                : throw new InvalidOperationException();
        }

        public TBuilderInterface FromAllDataSources()
        {
            EnsureQueryWasStarted();

            _query.DataSources = _dataSourceCollection!.DataSources
                .Select(x => x.RemoteStorageInfo?.Id)
                .ToList();

            return this is TBuilderInterface builderInterface
                ? builderInterface
                : throw new InvalidOperationException();
        }

        public TBuilderInterface GetAll()
        {
            EnsureQueryWasStarted();

            if (_query.Id != null)
                throw new InvalidQueryException("Requesting all data is not possible after specific id is selected.");

            _query.GetAllData = true;

            return this is TBuilderInterface builderInterface
                ? builderInterface
                : throw new InvalidOperationException();
        }

        public TBuilderInterface WithId(Guid id)
        {
            EnsureQueryWasStarted();

            if (_query.GetAllData)
                throw new InvalidQueryException("Specifying id is not possible after requesting all data.");

            _query.Id = id;

            return this is TBuilderInterface builderInterface
                ? builderInterface
                : throw new InvalidOperationException();
        }

        public abstract List<TEntity> ToList();
    }
}
