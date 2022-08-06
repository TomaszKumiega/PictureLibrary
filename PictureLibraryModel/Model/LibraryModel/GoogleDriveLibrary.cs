using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Linq;

namespace PictureLibraryModel.Model.LibraryModel
{
    public class GoogleDriveLibrary : RemoteLibrary
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IGoogleDriveAPIClient _client;

        public string FileId { get; set; }

        public GoogleDriveLibrary(
            ISettingsProvider settingsProvider,
            IGoogleDriveAPIClient googleDriveAPIClient) : base()
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
