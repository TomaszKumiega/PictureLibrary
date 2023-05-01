using System.Net.Mime;

namespace PictureLibraryModel.Model
{
    public class GoogleDriveImageFile : RemoteImageFile
    {
        public string? FileId { get; set; }
        public string? LibraryFolderId { get; set; }
        public string? ImagesFolderId { get; set; }

        public string GetMimeType()
        {
            return Extension?.ToLower() switch
            {
                ".jpg" or ".jpeg" => MediaTypeNames.Image.Jpeg,
                ".png" => "image/png",
                ".gif" => MediaTypeNames.Image.Gif,
                ".bmp" => "image/bmp",
                ".tiff" => MediaTypeNames.Image.Tiff,
                _ => "image/*",
            };
        }
    }
}
