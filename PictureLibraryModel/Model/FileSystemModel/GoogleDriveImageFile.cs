using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Linq;

namespace PictureLibraryModel.Model.FileSystemModel
{
    public class GoogleDriveImageFile : RemoteImageFile
    {
        private readonly IGoogleDriveAPIClient _client;
        private readonly ISettingsProvider _settingsProvider;

        public string FileId { get; set; }
        public string LibraryFolderId { get; set; }
        public string ImagesFolderId { get; set; }

        public GoogleDriveImageFile(
            IGoogleDriveAPIClient googleDriveAPIClient,
            ISettingsProvider settingsProvider) : base()
        {
            _client = googleDriveAPIClient;
            _settingsProvider = settingsProvider;
        }

        public override void LoadIcon()
        {
            if (_settingsProvider.Settings.RemoteStorageInfos.FirstOrDefault(x => x.Id == RemoteStorageInfoId) is GoogleDriveRemoteStorageInfo googleDriveRemoteStorageInfo)
            {
                var fileMetadata = _client.GetFileMetadata(googleDriveRemoteStorageInfo.UserName, FileId, $"files({FileId}, hasThumbnail, contentHints)");

                if (fileMetadata != null)
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
