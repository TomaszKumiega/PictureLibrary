using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.DataStoreInfos
{
    public interface IDataStoreInfoService
    {
        TDataStoreInfo? GetDataStoreInfo<TDataStoreInfo>(Guid id) where TDataStoreInfo : class, IDataStoreInfo;
        bool AddDataStoreInfo<TDataStoreInfo>(TDataStoreInfo dataStoreInfo) where TDataStoreInfo : class, IDataStoreInfo;
    }
}
