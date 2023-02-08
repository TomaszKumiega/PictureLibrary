using PictureLibrary.DataAccess.DataSource;
using PictureLibrary.DataAccess.ImageProvider;
using PictureLibrary.DataAccess.LibraryProvider;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Builders
{
    public class LocalDataSourceBuilder : IDataSourceBuilder
    {
        private readonly Func<IDataSource> _dataSourceLocator;
        private readonly Func<LocalLibraryProvider> _localLibraryProviderLocator;
        private readonly Func<LocalImageFileProvider> _localImageFileProviderLocator;

        private LocalImageFileProvider? _localImageFileProvider;
        private LocalLibraryProvider? _localLibraryProvider;
        private IDataSource? _dataSource;

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
            if (_dataSource == null)
                throw new InvalidOperationException("Data source should be created before creation of ImageFileProvider");

            _localImageFileProvider = _localImageFileProviderLocator();
            _dataSource.ImageProvider = _localImageFileProvider;

            return this;
        }

        public IDataSourceBuilder WithLibraryProvider()
        {
            if (_dataSource == null)
                throw new InvalidOperationException("Data source should be created before creation of LibraryProvider");

            _localLibraryProvider = _localLibraryProviderLocator();
            _dataSource!.LibraryProvider = _localLibraryProvider;

            return this;
        }

        public IDataSourceBuilder WithRemoteStorageInfo(IDataStoreInfo? remoteStorageInfo)
        {
            if (_dataSource == null)
                throw new InvalidOperationException("Data source should be created before setting remote storage info");

            if (remoteStorageInfo == null)
                return this;

            _dataSource!.RemoteStorageInfo = remoteStorageInfo!;

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