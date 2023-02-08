using PictureLibrary.FileSystem.API;
using PictureLibraryModel.Model.Settings;
using System.Globalization;
using System.Xml.Serialization;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IFileService _fileService;
        private readonly IPathFinder _appFolder;

        public SettingsProvider(
            IFileService fileService,
            IPathFinder appFolder)
        {
            _fileService = fileService;
            _appFolder = appFolder;
        }

        private Settings? _settings;
        public Settings? Settings
        {
            get
            {
                if (_settings is null)
                    LoadSettings();

                return _settings;
            }

            private set => _settings = value;
        }

        private string SettingsFileName 
            => _appFolder.AppFolderPath + "settings.xml";

        private void LoadSettings()
        {
            if (!_fileService.Exists(SettingsFileName))
            {
                Settings =
                    new Settings()
                    {
                        AccentColor = "#0066ff",
                        Language = CultureInfo.CurrentCulture.Name,
                        LightMode = true,
                        ImportedLocalLibraries = new List<string>(),
                    };
            }
            else
            {
                using var fileStream = _fileService.Open(SettingsFileName);

                var serializer = new XmlSerializer(typeof(Settings));
                Settings = serializer.Deserialize(fileStream) as Settings;

                fileStream.Close();
            }
        }

        public void SaveSettings()
        {
            if (Settings is null) 
                return;

            _fileService.Create(SettingsFileName);

            using var fileStream = _fileService.Open(SettingsFileName);

            var serializer = new XmlSerializer(typeof(Settings));
            serializer.Serialize(fileStream, Settings);

            fileStream.Close();
        }
    }
}
