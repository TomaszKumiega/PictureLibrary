using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.DataSource
{
    public interface IDataSourceCollection
    {
        IList<IDataSource> DataSources { get; }

        void Initialize(IEnumerable<IDataStoreInfo> remoteStorageInfos);
        IDataSource GetDataSourceByRemoteStorageId(Guid? remoteStorageId = null);
    }
}
