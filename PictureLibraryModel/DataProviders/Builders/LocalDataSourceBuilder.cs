using PictureLibraryModel.Model.RemoteStorages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.DataProviders.Builders
{
    public class LocalDataSourceBuilder : IDataSourceBuilder
    {
        private IDataSource DataSource { get; set; }
        private Func<IDataSource> DataSourceLocator { get; }
        private Func<LocalLibraryProvider> LocalLibraryProviderLocator { get; }
        private Func<LocalImageFileProvider> LocalImageFileProviderLocator { get; }

        public LocalDataSourceBuilder(Func<IDataSource> dataSourceLocator, Func<LocalImageFileProvider> localImageFileProviderLocator, Func<LocalLibraryProvider> localLibraryProviderLocator)
        {
            DataSourceLocator = dataSourceLocator;
            LocalImageFileProviderLocator = localImageFileProviderLocator;
            LocalLibraryProviderLocator = localLibraryProviderLocator;
        }

        public IDataSourceBuilder CreateDataSource()
        {
            DataSource = DataSourceLocator();

            return this;
        }

        public IDataSourceBuilder WithImageFileProvider()
        {
            DataSource.ImageProvider = LocalImageFileProviderLocator();
            return this;
        }

        public IDataSourceBuilder WithLibraryProvider()
        {
            DataSource.LibraryProvider = LocalLibraryProviderLocator();
            return this;
        }

        public IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo remoteStorageInfo)
        {
            DataSource.RemoteStorageInfo = remoteStorageInfo;
            return this;
        }

        public IDataSource Build()
        {
            return DataSource;
        }
    }
}