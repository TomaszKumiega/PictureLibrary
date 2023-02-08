namespace PictureLibraryModel.Model
{
    public class GoogleDriveImageFile : RemoteImageFile
    {
        public string FileId { get; set; }
        public string LibraryFolderId { get; set; }
        public string ImagesFolderId { get; set; }
    }
}
