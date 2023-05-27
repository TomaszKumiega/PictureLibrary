namespace PictureLibrary.FileSystem.API
{
    public class PathFinder : IPathFinder
    {
        public string AppFolderPath => Microsoft.Maui.Storage.FileSystem.Current.AppDataDirectory;

        public string GetDataStoreInfoFilePath(Type typeOfDataStoreInfo)
        {
            return AppFolderPath + Path.DirectorySeparatorChar + $"{typeOfDataStoreInfo.Name}.dsi";
        }

        public string GetSettingsFilePath(Type settingsType)
        {
            return AppFolderPath + Path.DirectorySeparatorChar + $"{settingsType.Name}.json";
        }

        public string GetDefaultLibrariesFolderPath()
        {
            return AppFolderPath + Path.DirectorySeparatorChar + "Libraries";
        }
    }
}
