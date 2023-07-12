using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.DataStoreInfos
{
    public interface IDataStoreInfoService
    {
        IEnumerable<IDataStoreInfo> GetAllDataStoreInfos();
        IEnumerable<TDataStoreInfo> GetAllDataStoreInfosOfType<TDataStoreInfo>() where TDataStoreInfo : class, IDataStoreInfo;
        TDataStoreInfo? GetDataStoreInfo<TDataStoreInfo>(Guid id) where TDataStoreInfo : class, IDataStoreInfo;
        IDataStoreInfo? GetDataStoreInfoFromLibrary(Library library);
        DataStoreType GetDataStoreTypeFromLibrary(Library library);
        bool AddDataStoreInfo<TDataStoreInfo>(TDataStoreInfo dataStoreInfo) where TDataStoreInfo : class, IDataStoreInfo;
    }
}
