using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Builders.Creators
{
    public class DataSourceCreator : IDataSourceCreator
    {
        private IImplementationSelector<DataSourceType, IDataSourceBuilder> BuildersImplementations { get; }

        public DataSourceCreator(IImplementationSelector<DataSourceType, IDataSourceBuilder> buildersImplementations)
        {
            BuildersImplementations = buildersImplementations;
        }

        public IDataSource CreateDataSource(IDataStoreInfo? remoteStorageInfo = null)
        {
            DataSourceType storageType = remoteStorageInfo != null
                ? remoteStorageInfo.DataSourceType
                : DataSourceType.Local;

            var builder = BuildersImplementations.Select(storageType);

            return builder
                .CreateDataSource()
                .WithImageFileProvider()
                .WithLibraryProvider()
                .WithRemoteStorageInfo(remoteStorageInfo)
                .Build();
        }
    }
}
