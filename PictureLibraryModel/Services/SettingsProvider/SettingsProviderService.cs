using NLog;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public class SettingsProviderService : ISettingsProviderService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IFileService FileService { get; }
        private IImplementationSelector<RemoteStorageTypes, IRemoteStorageInfo> RemoteStorageImplementationSelector { get; }

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

        public SettingsProviderService(IFileService fileService, IImplementationSelector<RemoteStorageTypes, IRemoteStorageInfo> implementationSelector)
        {
            FileService = fileService;
            RemoteStorageImplementationSelector = implementationSelector;
        }

        private void LoadSettings()
        {
            if (!FileService.Exists("settings.xml") || !IsFileXml("settings.xml"))
            {
                Settings =
                    new Settings()
                    {
                        AccentColor = "#0066ff",
                        Language = CultureInfo.CurrentCulture.Name,
                        LightMode = true,
                        ImportedLibraries = new List<string>()
                    };
            }
            else
            {
                var settings = new Settings();
                var fileStream = FileService.OpenFile("settings.xml", FileMode.Open, FileAccess.Read, FileShare.Read);

                XmlReaderSettings xmlSettings = new XmlReaderSettings();
                xmlSettings.DtdProcessing = DtdProcessing.Parse;

                settings.ImportedLibraries = new List<string>();

                using (var reader = XmlReader.Create(fileStream, xmlSettings))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case "accent_color":
                                    {
                                        var accentColorElement = XNode.ReadFrom(reader) as XElement;
                                        settings.AccentColor = accentColorElement.Attribute("value").Value;
                                    }
                                    break;
                                case "language":
                                    {
                                        var languageElement = XNode.ReadFrom(reader) as XElement;
                                        settings.Language = languageElement.Attribute("value").Value;
                                    }
                                    break;
                                case "light_mode":
                                    {
                                        var lightModeElement = XNode.ReadFrom(reader) as XElement;
                                        settings.LightMode = Convert.ToBoolean(lightModeElement.Attribute("value").Value);
                                    }
                                    break;
                                case "library":
                                    {
                                        var libraryElement = XNode.ReadFrom(reader) as XElement;
                                        settings.ImportedLibraries.Add(libraryElement.Attribute("path").Value);
                                    }
                                    break;
                                case "remote_storage_info":
                                    {
                                        var remoteStorageInfoElement = XNode.ReadFrom(reader) as XElement;
                                        
                                        if (int.TryParse(remoteStorageInfoElement.Attribute("type").Value, out int remoteStorageTypeInt))
                                        {
                                            var remoteStorageInfo = LoadRemoteStorageInfoFromString((RemoteStorageTypes)remoteStorageTypeInt, remoteStorageInfoElement.Value);
                                            settings.RemoteStorageInfos.Add(remoteStorageInfo);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

                Settings = settings;
            }
        }

        public async Task SaveSettingsAsync()
        {
            if (Settings == null) return;

            var settings = new XElement("settings");

            var accentColor = new XElement("accent_color", new XAttribute("value", Settings.AccentColor));
            var language = new XElement("language", new XAttribute("value", Settings.Language));
            var lightMode = new XElement("light_mode", new XAttribute("value", Settings.LightMode));
            var importedLibraries = new XElement("imported_libraries");
            var remoteStorageInfos = new XElement("remote_storage_infos");

            foreach (var t in Settings.ImportedLibraries)
            {
                var libraryElement = new XElement("library", new XAttribute("path", t));
                importedLibraries.Add(libraryElement);
            }

            foreach (var t in Settings.RemoteStorageInfos)
            {
                var remoteStorageInfoElement = new XElement("remote_storage_info", new XAttribute("type", (byte)t.StorageType));
                remoteStorageInfos.Add(remoteStorageInfoElement);
            }

            settings.Add(accentColor);
            settings.Add(language);
            settings.Add(lightMode);
            settings.Add(importedLibraries);
            settings.Add(remoteStorageInfos);

            try
            {
                FileService.Create("settings.xml");
                var fileStream = FileService.OpenFile("settings.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var xmlWriter = new XmlTextWriter(streamWriter);

                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 4;

                    await Task.Run(() => settings.Save(xmlWriter));
                }

                fileStream.Close();
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Couldn't save the settings");
            }
        }

        private bool IsFileXml(string path)
        {
            try
            {
                var doc = XDocument.Load(path);
            }
            catch(XmlException)
            {
                return false;
            }

            return true;
        }

        private IRemoteStorageInfo LoadRemoteStorageInfoFromString(RemoteStorageTypes remoteStorageType, string serializedRemoteStorageInfo)
        {
            IRemoteStorageInfo remoteStorageInfo = RemoteStorageImplementationSelector.Select(remoteStorageType);
            remoteStorageInfo.Deserialize(serializedRemoteStorageInfo);
            
            return remoteStorageInfo;
        }
    }
}
