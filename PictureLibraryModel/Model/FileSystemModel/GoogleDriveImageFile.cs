using System;
using System.Linq;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    public class GoogleDriveImageFile : RemoteImageFile
    {
        [XmlIgnore]
        public IGoogleDriveApiClient Client { private get; set; }
        [XmlIgnore]
        public ISettingsProvider SettingsProvider { private get; set; }

        public string FileId { get; set; }
        public string LibraryFolderId { get; set; }
        public string ImagesFolderId { get; set; }

        public GoogleDriveImageFile()
        {

        }

        public GoogleDriveImageFile(
            IGoogleDriveApiClient googleDriveAPIClient,
            ISettingsProvider settingsProvider) : base()
        {
            Client = googleDriveAPIClient;
            SettingsProvider = settingsProvider;
        }

        public override void LoadIcon()
        {
            if (SettingsProvider.Settings.RemoteStorageInfos.FirstOrDefault(x => x.Id == RemoteStorageInfoId) is GoogleDriveDataStoreInfo googleDriveRemoteStorageInfo)
            {
                var fileMetadata = Client.GetFileMetadata(googleDriveRemoteStorageInfo.UserName, FileId, $"hasThumbnail,thumbnailLink");
                bool hasThumbail = fileMetadata?.HasThumbnail ?? false;

                if (hasThumbail)
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
