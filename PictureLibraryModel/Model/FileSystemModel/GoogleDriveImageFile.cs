using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model.FileSystemModel
{
    public class GoogleDriveImageFile : RemoteImageFile
    {
        [XmlIgnore]
        public IGoogleDriveAPIClient Client { private get; set; }
        [XmlIgnore]
        public ISettingsProvider SettingsProvider { private get; set; }

        public string FileId { get; set; }
        public string LibraryFolderId { get; set; }
        public string ImagesFolderId { get; set; }

        public GoogleDriveImageFile()
        {

        }

        public GoogleDriveImageFile(
            IGoogleDriveAPIClient googleDriveAPIClient,
            ISettingsProvider settingsProvider) : base()
        {
            Client = googleDriveAPIClient;
            SettingsProvider = settingsProvider;
        }

        public override void LoadIcon()
        {
            if (SettingsProvider.Settings.RemoteStorageInfos.FirstOrDefault(x => x.Id == RemoteStorageInfoId) is GoogleDriveRemoteStorageInfo googleDriveRemoteStorageInfo)
            {
                var fileMetadata = Client.GetFileMetadata(googleDriveRemoteStorageInfo.UserName, FileId, $"hasThumbnail,thumbnailLink");
                bool hasThumbail = fileMetadata.HasThumbnail.HasValue ? fileMetadata.HasThumbnail.Value : false;

                if (fileMetadata != null && hasThumbail)
                {
                    var bytes = Convert.FromBase64String(fileMetadata.ContentHints.Thumbnail.Image);
                    Icon = new ImageMagick.MagickImage(bytes);
                    return;
                }
            }

            Icon = new ImageMagick.MagickImage();
        }
    }
}
