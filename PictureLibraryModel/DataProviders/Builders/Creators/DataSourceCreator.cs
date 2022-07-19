using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders.Builders
{
    public class DataSourceCreator : IDataSourceCreator
    {
        private IImplementationSelector<DataSourceType, IDataSourceBuilder> BuildersImplementations { get; }

        public DataSourceCreator(IImplementationSelector<DataSourceType, IDataSourceBuilder> buildersImplementations)
        {
            BuildersImplementations = buildersImplementations;
        }

        public IDataSource CreateDataSource(IRemoteStorageInfo remoteStorageInfo = null)
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
