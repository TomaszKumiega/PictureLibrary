using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureLibraryModel.DataProviders
{
    public class DataSourceCollection : IDataSourceCollection
    {
        private IDataSourceCreator DataSourceCreator { get; }

        public IList<IDataSource> DataSources { get; private set; }

        public DataSourceCollection(IDataSourceCreator dataSourceCreator)
        {
            DataSources = new List<IDataSource>();
            DataSourceCreator = dataSourceCreator;
        }

        public void Initialize(IEnumerable<IRemoteStorageInfo> remoteStorageInfos)
        {
            if (DataSources.Any())
                DataSources.Clear();

            var localDataSource = DataSourceCreator.CreateDataSource();
            DataSources.Add(localDataSource);

            foreach (var remoteStorageInfo in remoteStorageInfos)
            {
                DataSources.Add(DataSourceCreator.CreateDataSource(remoteStorageInfo));
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
