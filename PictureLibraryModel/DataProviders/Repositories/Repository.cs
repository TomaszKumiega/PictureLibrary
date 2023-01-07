using PictureLibraryModel.Model;
using System;

namespace PictureLibraryModel.DataProviders.Repositories
{
    public class Repository<T>
        where T : class
    {
        protected IDataSourceCollection _dataSourceCollection;

        private Guid? GetRemoteStorageId(object entity)
            => entity is IRemoteEntity remoteEntity
                ? remoteEntity.RemoteStorageInfoId
                : null;

        protected IDataSource GetDataSource(T entity)
        {
            return GetDataSource<T>(entity);
        }

        protected IDataSource GetDataSource<TEntity>(TEntity entity)
            where TEntity : class
        {
            Guid? remoteStorageId = GetRemoteStorageId(entity);
            return _dataSourceCollection.GetDataSourceByRemoteStorageId(remoteStorageId);
        }
    }
}
