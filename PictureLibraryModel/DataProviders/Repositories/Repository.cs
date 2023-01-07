using PictureLibraryModel.Model;
using System;

namespace PictureLibraryModel.DataProviders.Repositories
{
    public class Repository<T>
    {
        protected Guid? GetRemoteStorageId(T entity)
            => entity is IRemoteEntity remoteEntity
                ? remoteEntity.RemoteStorageInfoId
                : null;
    }
}
