namespace PictureLibraryModel.Model.RemoteStorages
{
    public enum DataSourceType : byte
    {
        Local = 0,
        PictureLibraryAPI = 1,
        GoogleDrive = 2,
        
        Remote = PictureLibraryAPI | GoogleDrive,
    }
}
