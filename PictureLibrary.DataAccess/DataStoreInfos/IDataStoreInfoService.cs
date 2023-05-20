using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.DataStoreInfos
{
    public interface IDataStoreInfoService
    {
        IEnumerable<TDataStoreInfo> GetAllDataStoreInfosOfType<TDataStoreInfo>() where TDataStoreInfo : class, IDataStoreInfo;
        TDataStoreInfo? GetDataStoreInfo<TDataStoreInfo>(Guid id) where TDataStoreInfo : class, IDataStoreInfo;
        bool AddDataStoreInfo<TDataStoreInfo>(TDataStoreInfo dataStoreInfo) where TDataStoreInfo : class, IDataStoreInfo;
    }
}
