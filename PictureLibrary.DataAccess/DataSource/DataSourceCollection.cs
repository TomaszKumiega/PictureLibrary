using PictureLibrary.DataAccess.Builders.Creators;
using PictureLibraryModel.Exceptions;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.SettingsProvider;

namespace PictureLibrary.DataAccess.DataSource
{
    public class DataSourceCollection : IDataSourceCollection
    {
        private readonly IDataSourceCreator _dataSourceCreator;

        public IList<IDataSource> DataSources { get; private set; }

        public DataSourceCollection(
            IDataSourceCreator dataSourceCreator)
        {
            DataSources = new List<IDataSource>();
            _dataSourceCreator = dataSourceCreator;

            Initialize(settingsProvider.Settings.RemoteStorageInfos);
        }

        public void Initialize(IEnumerable<IDataStoreInfo> remoteStorageInfos)
        {
            if (DataSources.Any())
            {
                DataSources.Clear();
            }

            var localDataSource = _dataSourceCreator.CreateDataSource();
            DataSources.Add(localDataSource);

            foreach (var remoteStorageInfo in remoteStorageInfos)
            {
                DataSources.Add(_dataSourceCreator.CreateDataSource(remoteStorageInfo));
            }
        }

        public IDataSource GetDataSourceByRemoteStorageId(Guid? remoteStorageId = null)
        {
            var dataSource = DataSources.FirstOrDefault(x => x.RemoteStorageInfo?.Id == remoteStorageId);

            if (dataSource == null)
            {
                throw new DataSourceNotFoundException(remoteStorageId);
            }

            return dataSource;
        }
    }
}
