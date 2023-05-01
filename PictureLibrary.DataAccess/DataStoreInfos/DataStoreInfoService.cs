using PictureLibrary.FileSystem.API;

namespace PictureLibrary.DataAccess
{
    internal class DataStoreInfoProvider
    {
        private readonly IPathFinder _pathFinder;

        public DataStoreInfoProvider(
            IPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        private string DataStoreInfoFilePath
            => _pathFinder.AppFolderPath + "DSI.plef";


    }
}
