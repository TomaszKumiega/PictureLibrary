using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.Model.RemoteStorages;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders
{
    public class DataSourceCollection : IDataSourceCollection
    {
        private IDataSourceCreator DataSourceCreator { get; }

        public IList<IDataSource> DataSources { get; private set; }

        public DataSourceCollection(IDataSourceCreator dataSourceCreator)
        {
            DataSourceCreator = dataSourceCreator;
        }

        public void Initialize(IEnumerable<IRemoteStorageInfo> remoteStorageInfos)
        {
            var localDataSource = DataSourceCreator.CreateDataSource();
            DataSources.Add(localDataSource);

            foreach (var remoteStorageInfo in remoteStorageInfos)
            {
                DataSources.Add(DataSourceCreator.CreateDataSource(remoteStorageInfo));
            }
        }
    }
}
