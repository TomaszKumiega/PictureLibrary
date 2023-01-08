using PictureLibraryModel.Model;
using System;

namespace PictureLibraryModel.DataProviders.Builders
{
    public class LocalDataSourceBuilder : IDataSourceBuilder
    {
        private readonly Func<IDataSource> _dataSourceLocator;
        private readonly Func<LocalLibraryProvider> _localLibraryProviderLocator;
        private readonly Func<LocalImageFileProvider> _localImageFileProviderLocator;

        private IDataSource _dataSource;

        public LocalDataSourceBuilder(
            Func<IDataSource> dataSourceLocator, 
            Func<LocalImageFileProvider> localImageFileProviderLocator, 
            Func<LocalLibraryProvider> localLibraryProviderLocator)
        {
            _dataSourceLocator = dataSourceLocator;
            _localImageFileProviderLocator = localImageFileProviderLocator;
            _localLibraryProviderLocator = localLibraryProviderLocator;
        }

        public IDataSourceBuilder CreateDataSource()
        {
            _dataSource = _dataSourceLocator();

            return this;
        }

        public IDataSourceBuilder WithImageFileProvider()
        {
            _dataSource.ImageProvider = _localImageFileProviderLocator();
            return this;
        }

        public IDataSourceBuilder WithLibraryProvider()
        {
            _dataSource.LibraryProvider = _localLibraryProviderLocator();
            return this;
        }

        public IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo remoteStorageInfo)
        {
            _dataSource.RemoteStorageInfo = remoteStorageInfo;
            return this;
        }

        public IDataSource Build()
        {
            return _dataSource;
        }
    }
}