namespace PictureLibrary.FileSystem.API
{
    public interface IPathFinder
    {
        string AppFolderPath { get; }
        string GetDataStoreInfoFilePath(Type typeOfDataStoreInfo);
        string GetSettingsFilePath(Type settingsType);
        string GetDefaultLibrariesFolderPath();
    }
}
