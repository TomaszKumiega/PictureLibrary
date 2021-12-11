using PictureLibraryModel.Model.RemoteStorages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.DataProviders.Builders
{
    public class LocalDataSourceBuilder : IDataSourceBuilder
    {
        private IDataSource DataSource { get; }
        private Func<LocalLibraryProvider> LocalLibraryProviderLocator { get; }
        private Func<LocalImageFileProvider> LocalImageFileProviderLocator { get; }

        public LocalDataSourceBuilder(IDataSource dataSource, Func<LocalImageFileProvider> localImageFileProviderLocator, Func<LocalLibraryProvider> localLibraryProviderLocator)
        {
            DataSource = dataSource;
            LocalImageFileProviderLocator = localImageFileProviderLocator;
            LocalLibraryProviderLocator = localLibraryProviderLocator;
        }

        public IDataSourceBuilder CreateImageFileProvider()
        {
            DataSource.ImageProvider = LocalImageFileProviderLocator();
            return this;
        }

        public IDataSourceBuilder CreateLibraryProvider()
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