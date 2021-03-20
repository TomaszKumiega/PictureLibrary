using NLog;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public class SettingsProviderService : ISettingsProviderService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IFileService FileService { get; }

        public Settings Settings { get; private set; }

        public SettingsProviderService(IFileService fileService)
        {
            FileService = fileService;
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (!FileService.Exists("settings.xml"))
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
                var fileStream = FileService.OpenFile("settings.xml");

                XmlReaderSettings xmlSettings = new XmlReaderSettings();
                xmlSettings.DtdProcessing = DtdProcessing.Parse;

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
                                        if (settings.ImportedLibraries == null) settings.ImportedLibraries = new List<string>();
                                        var libraryElement = XNode.ReadFrom(reader) as XElement;
                                        settings.ImportedLibraries.Add(libraryElement.Attribute("path").Value);
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

            foreach (var t in Settings.ImportedLibraries)
            {
                var libraryElement = new XElement("library", new XAttribute("path", t));
                importedLibraries.Add(libraryElement);
            }

            settings.Add(accentColor);
            settings.Add(language);
            settings.Add(lightMode);
            settings.Add(importedLibraries);

            try
            {
                if(!FileService.Exists("settings.xml")) FileService.Create("settings.xml");
                var fileStream = FileService.OpenFile("settings.xml");

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
    }
}
