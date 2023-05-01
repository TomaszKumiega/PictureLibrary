using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.DataStoreInfos
{
    public interface IDataStoreInfoProvider
    {
        TDataStoreInfo GetDataStoreInfo<TDataStoreInfo>(Guid id) where TDataStoreInfo : class, IDataStoreInfo;
    }
}
