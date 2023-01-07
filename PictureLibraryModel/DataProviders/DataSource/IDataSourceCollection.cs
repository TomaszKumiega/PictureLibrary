using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders
{
    public interface IDataSourceCollection
    {
        IList<IDataSource> DataSources { get; }

        void Initialize(IEnumerable<IRemoteStorageInfo> remoteStorageInfos);
        IDataSource GetDataSourceByRemoteStorageId(Guid? remoteStorageId = null);
    }
}
