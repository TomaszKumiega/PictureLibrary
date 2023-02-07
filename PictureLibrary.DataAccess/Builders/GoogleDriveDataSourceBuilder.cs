using PictureLibrary.DataAccess.DataSource;
using PictureLibrary.DataAccess.ImageProvider;
using PictureLibrary.DataAccess.LibraryProvider;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.DataAccess.Builders
{
    public class GoogleDriveDataSourceBuilder : IDataSourceBuilder
    {
        private readonly Func<IDataSource> _dataSourceLocator;
        private readonly Func<GoogleDriveLibraryProvider> _googleDriveLibraryProvider;
        private readonly Func<GoogleDriveImageFileProvider> _googleDriveImageFileProvider;

        private GoogleDriveImageFileProvider? _imageProvider;
        private GoogleDriveLibraryProvider? _libraryProvider;
        private IDataSource? _dataSource;

        public GoogleDriveDataSourceBuilder(
            Func<IDataSource> dataSourceLocator,
            Func<GoogleDriveImageFileProvider> googleDriveImageFileProvider,
            Func<GoogleDriveLibraryProvider> googleDriveLibraryProvider)
        {
            _dataSourceLocator = dataSourceLocator;
            _googleDriveImageFileProvider = googleDriveImageFileProvider;
            _googleDriveLibraryProvider = googleDriveLibraryProvider;
        }

        public IDataSourceBuilder CreateDataSource()
        {
            _dataSource = _dataSourceLocator();

            return this;
        }

        public IDataSourceBuilder WithImageFileProvider()
        {
            if (_dataSource == null)
                throw new InvalidOperationException("Data source should be created before creation of ImageFileProvider");

            _imageProvider = _googleDriveImageFileProvider();
            _dataSource.ImageProvider = _imageProvider;

            return this;
        }

        public IDataSourceBuilder WithLibraryProvider()
        {
            if (_dataSource == null)
                throw new InvalidOperationException("Data source should be created before creation of LibraryProvider");

            _libraryProvider = _googleDriveLibraryProvider();
            _dataSource!.LibraryProvider = _libraryProvider;

            return this;
        }

        public IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo? remoteStorageInfo)
        {
            if (_libraryProvider == null || _imageProvider == null)
                throw new InvalidOperationException("LibraryProvider and ImageFileProvider should be created before setting remote storage info");

            if (remoteStorageInfo == null)
                return this;

            _dataSource!.RemoteStorageInfo = remoteStorageInfo;
            _imageProvider.RemoteStorageInfo = (GoogleDriveRemoteStorageInfo)remoteStorageInfo;
            _libraryProvider.RemoteStorageInfo = (GoogleDriveRemoteStorageInfo)remoteStorageInfo;

            return this;
        }

        public IDataSource Build()
        {
            if (_dataSource?.RemoteStorageInfo == default)
                throw new InvalidOperationException("Not enough data to create an object");

            return _dataSource;
        }
    }
}
