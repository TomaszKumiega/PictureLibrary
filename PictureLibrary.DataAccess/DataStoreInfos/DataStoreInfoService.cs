using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.FileSystem.API;

namespace PictureLibrary.DataAccess
{
    internal class DataStoreInfoService : IDataStoreInfoService
    {
        private readonly IPathFinder _pathFinder;

        public DataStoreInfoService(
            IPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        TDataStoreInfo IDataStoreInfoService.GetDataStoreInfo<TDataStoreInfo>(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
