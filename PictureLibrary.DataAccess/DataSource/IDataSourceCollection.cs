using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.DataSource
{
    public interface IDataSourceCollection
    {
        IList<IDataSource> DataSources { get; }

        void Initialize(IEnumerable<IRemoteStorageInfo> remoteStorageInfos);
        IDataSource GetDataSourceByRemoteStorageId(Guid? remoteStorageId = null);
    }
}
