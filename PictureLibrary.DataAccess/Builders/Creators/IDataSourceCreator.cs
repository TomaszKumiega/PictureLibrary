using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Builders.Creators
{
    public interface IDataSourceCreator
    {
        IDataSource CreateDataSource(IDataStoreInfo? remoteStorageInfo = null);
    }
}
