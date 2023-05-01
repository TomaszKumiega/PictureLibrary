namespace PictureLibraryModel.Model
{
    public class GoogleDriveLibrary : RemoteLibrary
    {
        public string? FileId { get; set; }
        public string? LibraryFolderId { get; set; }
        public string? ImagesFolderId { get; set; }
    }
}
