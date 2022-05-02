using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.RemoteStorageInfoSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IRemoteStorageInfosSerializer _remoteStorageInfosSerializer;
        private readonly Func<SerializableSettings> _serializableSettingsLocator;
        private readonly Func<Settings> _settingsLocator;
        private readonly IFileService _fileService;


        private Settings _settings;
        public Settings Settings 
        { 
            get
            {
                if (_settings == null)
                    LoadSettings();

                return _settings;
            }

            private set => _settings = value; 
        }

        public SettingsProvider( 
            IRemoteStorageInfosSerializer remoteStorageInfosSerializer, 
            Func<SerializableSettings> serializableSettingsLocator, 
            Func<Settings> settingsLocator,
            IFileService fileService)
        {
            _remoteStorageInfosSerializer = remoteStorageInfosSerializer;
            _serializableSettingsLocator = serializableSettingsLocator;
            _settingsLocator = settingsLocator;
            _fileService = fileService;
        }

        private void LoadSettings()
        {
            if (!_fileService.Exists("settings.xml"))
            {
                Settings =
                    new Settings()
                    {
                        AccentColor = "#0066ff",
                        Language = CultureInfo.CurrentCulture.Name,
                        LightMode = true,
                        ImportedLocalLibraries = new List<string>(),
                        RemoteStorageInfos = new List<IRemoteStorageInfo>()
                    };
            }
            else
            {
                using (var fileStream = _fileService.OpenFile("settings.xml", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    var serializer = new XmlSerializer(typeof(SerializableSettings));
                    var serializableSettings = (SerializableSettings)serializer.Deserialize(fileStream);

                    Settings = serializableSettings.GetSettings(_remoteStorageInfosSerializer, _settingsLocator);

                    fileStream.Close();
                }
            }
        }

        public void SaveSettings()
        {
            if (Settings == null) return;

            _fileService.Create("settings.xml");

            using (var fileStream = _fileService.OpenFile("settings.xml", FileMode.Open, FileAccess.Write, FileShare.None))
            {
                var serializableSettings = _serializableSettingsLocator();
                serializableSettings.Initialize(_remoteStorageInfosSerializer, Settings);

                var serializer = new XmlSerializer(typeof(SerializableSettings));
                serializer.Serialize(fileStream, serializableSettings);

                fileStream.Close();
            }            
        }
    }
}
