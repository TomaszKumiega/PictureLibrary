using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureLibraryModel.DataProviders
{
    public class DataSourceCollection : IDataSourceCollection
    {
        private readonly IDataSourceCreator _dataSourceCreator;

        public IList<IDataSource> DataSources { get; private set; }

        public DataSourceCollection(IDataSourceCreator dataSourceCreator)
        {
            DataSources = new List<IDataSource>();
            _dataSourceCreator = dataSourceCreator;
        }

        public void Initialize(IEnumerable<IRemoteStorageInfo> remoteStorageInfos)
        {
            if (DataSources.Any())
                DataSources.Clear();

            var localDataSource = _dataSourceCreator.CreateDataSource();
            DataSources.Add(localDataSource);

            foreach (var remoteStorageInfo in remoteStorageInfos)
            {
                DataSources.Add(_dataSourceCreator.CreateDataSource(remoteStorageInfo));
            }
        }

        public List<Library> GetAllLibraries()
        {
            var libraries = new List<Library>();

            foreach (var dataSource in DataSources)
            {
                libraries.AddRange(dataSource.LibraryProvider.GetAllLibraries());
            }

            return libraries;
        }

        public IDataSource GetDataSourceByRemoteStorageId(Guid? remoteStorageId)
        {
            return DataSources.FirstOrDefault(x => x.RemoteStorageInfo?.Id == remoteStorageId);
        }
    }
}
