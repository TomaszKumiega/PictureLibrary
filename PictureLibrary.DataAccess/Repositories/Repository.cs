using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Repositories
{
    public abstract class Repository<T>
        where T : class
    {
        public Repository(IDataSourceCollection dataSourceCollection)
        {
            _dataSourceCollection = dataSourceCollection;
        }

        protected IDataSourceCollection _dataSourceCollection;

        private static Guid? GetRemoteStorageId(object entity)
            => entity is IRemoteEntity remoteEntity
                ? remoteEntity.RemoteStorageInfoId
                : null;

        protected IDataSource GetDataSource(T entity)
            => GetDataSource<T>(entity);

        protected IDataSource GetDataSource<TEntity>(TEntity entity)
            where TEntity : class
        {
            Guid? remoteStorageId = GetRemoteStorageId(entity);
            return _dataSourceCollection.GetDataSourceByRemoteStorageId(remoteStorageId);
        }
    }
}
