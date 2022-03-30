using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.RemoteStorageInfoSerializer;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.Model.Settings
{
    public class SerializableSettings : SettingsBase
    {
        public List<SerializableRemoteStorageInfo> SerializedRemoteStorageInfos { get; set; }

        public void Initialize(IRemoteStorageInfosSerializer remoteStorageInfosSerializer, Settings settings)
        {
            LightMode = settings.LightMode;
            AccentColor = settings.AccentColor;
            Language = settings.Language;
            ImportedLocalLibraries = settings.ImportedLocalLibraries;
            SerializedRemoteStorageInfos = remoteStorageInfosSerializer.SerializeStorageInfos(settings.RemoteStorageInfos);
        }

        public Settings GetSettings(IRemoteStorageInfosSerializer remoteStorageInfosSerializer, Func<Settings> settingsLocator)
        {
            var settings = settingsLocator();

            settings.LightMode = LightMode;
            settings.AccentColor = AccentColor;
            settings.Language = Language;
            settings.ImportedLocalLibraries = ImportedLocalLibraries;
            settings.RemoteStorageInfos = remoteStorageInfosSerializer.DeserializeStorageInfos(SerializedRemoteStorageInfos);

            return settings;
        }
    }
}
