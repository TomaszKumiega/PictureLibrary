using PictureLibraryModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureLibraryModel.DataProviders.Queries
{
    public abstract class QueryBuilder<TBuilderInterface, TEntity> : IQueryBuilder<TBuilderInterface, TEntity>
        where TBuilderInterface : class
        where TEntity : class
    {
        protected readonly StorageQuery _query;
        protected IDataSourceCollection _dataSourceCollection;

        protected QueryBuilder(Func<StorageQuery> storageQueryLocator)
        {
            _query = storageQueryLocator();
        }

        public TBuilderInterface FromDataSources(params Guid?[] sources)
        {
            foreach (var sourceId in sources)
            {
                if (!_dataSourceCollection.DataSources.Any(x => x.RemoteStorageInfo.Id == sourceId))
                    throw new ArgumentException($"There is no data source with id {sourceId}");
            }

            _query.DataSources = sources.ToList();

            return this as TBuilderInterface;
        }

        public TBuilderInterface FromAllDataSources()
        {
            _query.DataSources = _dataSourceCollection.DataSources
                .Select(x => x.RemoteStorageInfo?.Id)
                .ToList();

            return this as TBuilderInterface;
        }

        public TBuilderInterface GetAll()
        {
            if (_query.Id != null)
                throw new InvalidQueryException("Requesting all data is not possible after specific id is selected.");

            _query.GetAllData = true;

            return this as TBuilderInterface;
        }

        public TBuilderInterface WithId(Guid id)
        {
            if (_query.GetAllData)
                throw new InvalidQueryException("Specifying id is not possible after requesting all data.");

            _query.Id = id;

            return this as TBuilderInterface;
        }

        public abstract List<TEntity> ToList();
    }
}
