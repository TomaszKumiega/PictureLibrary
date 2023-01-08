using PictureLibraryModel.DataProviders.ImageProvider;
using PictureLibraryModel.DataProviders.LibraryProvider;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using System;

namespace PictureLibraryModel.DataProviders.Builders
{
    public class GoogleDriveDataSourceBuilder : IDataSourceBuilder
    {
        private readonly Func<IDataSource> _dataSourceLocator;
        private readonly Func<GoogleDriveLibraryProvider> _googleDriveLibraryProvider;
        private readonly Func<GoogleDriveImageFileProvider> _googleDriveImageFileProvider;

        private IDataSource _dataSource;

        public GoogleDriveDataSourceBuilder(
            Func<IDataSource> dataSourceLocator, 
            Func<GoogleDriveImageFileProvider> googleDriveImageFileProvider, 
            Func<GoogleDriveLibraryProvider> googleDriveLibraryProvider)
        {
            _dataSourceLocator = dataSourceLocator;
            _googleDriveImageFileProvider = googleDriveImageFileProvider;
            _googleDriveLibraryProvider = googleDriveLibraryProvider;
        }

        public IDataSource Build()
        {
            return _dataSource;
        }

        public IDataSourceBuilder CreateDataSource()
        {
            _dataSource = _dataSourceLocator();

            return this;
        }

        public IDataSourceBuilder WithImageFileProvider()
        {
            _dataSource.ImageProvider = _googleDriveImageFileProvider();

            return this;
        }

        public IDataSourceBuilder WithLibraryProvider()
        {
            _dataSource.LibraryProvider = _googleDriveLibraryProvider();

            return this;
        }

        public IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo remoteStorageInfo)
        {
            _dataSource.RemoteStorageInfo = remoteStorageInfo;
            ((GoogleDriveImageFileProvider)_dataSource.ImageProvider).RemoteStorageInfo = (GoogleDriveRemoteStorageInfo)remoteStorageInfo;
            ((GoogleDriveLibraryProvider)_dataSource.LibraryProvider).RemoteStorageInfo = (GoogleDriveRemoteStorageInfo)remoteStorageInfo;

            return this;
        }
    }
}
