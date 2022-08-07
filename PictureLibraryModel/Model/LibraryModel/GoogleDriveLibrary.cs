using ImageMagick;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.SettingsProvider;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model.LibraryModel
{
    public class GoogleDriveLibrary : RemoteLibrary
    {
        [XmlIgnore]
        public ISettingsProvider SettingsProvider { private get; set; }
        [XmlIgnore]
        public IGoogleDriveAPIClient Client { private get; set; }

        public string FileId { get; set; }
        public string LibraryFolderId { get; set; }
        public string ImagesFolderId { get; set; }

        public GoogleDriveLibrary() : base() { }

        public GoogleDriveLibrary(
            ISettingsProvider settingsProvider,
            IGoogleDriveAPIClient googleDriveAPIClient) : base()
        {
            Client = googleDriveAPIClient;
            SettingsProvider = settingsProvider;
        }

        public override void LoadIcon()
        {
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;

            Icon = new MagickImage(".\\Icons\\LibraryIcon.png", settings);
        }
    }
}
