namespace PictureLibrary.FileSystem.API
{
    public class PathFinder : IPathFinder
    {
        public string AppFolderPath => GetType().Assembly.Location;

        public string GetDataStoreInfoFilePath(Type typeOfDataStoreInfo)
        {
            return AppFolderPath + Path.PathSeparator + $"{typeOfDataStoreInfo.Name}.dsi";
        }

        public string GetSettingsFilePath(Type settingsType)
        {
            return AppFolderPath + Path.PathSeparator + $"{settingsType.Name}.json";
        }
    }
}
