using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model.RemoteStorages;
using System;

namespace PictureLibraryModel.DataProviders.Builders
{
    public class DataSourceCreator : IDataSourceCreator
    {
        private IImplementationSelector<int, IDataSourceBuilder> BuildersImplementations { get; }

        public DataSourceCreator(IImplementationSelector<int, IDataSourceBuilder> buildersImplementations)
        {
            BuildersImplementations = buildersImplementations;
        }

        public IDataSource CreateDataSource(IRemoteStorageInfo remoteStorageInfo = null)
        {
            int storageType = remoteStorageInfo != null
                ? (int)remoteStorageInfo.StorageType
                : -1;

            var builder = BuildersImplementations.Select(storageType);

            return builder
                .CreateImageFileProvider()
                .CreateLibraryProvider()
                .WithRemoteStorageInfo(remoteStorageInfo)
                .Build();
        }
    }
}
