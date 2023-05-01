namespace PictureLibrary.FileSystem.API
{
    public interface IPathFinder
    {
        string AppFolderPath { get; }
        string DataStoreInfoFilePath { get; set; }
    }
}
