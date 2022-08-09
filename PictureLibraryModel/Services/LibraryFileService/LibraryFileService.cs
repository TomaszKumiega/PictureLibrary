using PictureLibraryModel.Model;
using PictureLibraryModel.Model.FileSystemModel;
using PictureLibraryModel.Model.LibraryModel;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.IO;
using System.Xml.Serialization;

namespace PictureLibraryModel.Services.LibraryFileService
{
    public class LibraryFileService : ILibraryFileService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IGoogleDriveAPIClient _googleDriveAPIClient;

        public LibraryFileService(
            ISettingsProvider settingsProvider,
            IGoogleDriveAPIClient googleDriveAPIClient)
        {
            _settingsProvider = settingsProvider;
            _googleDriveAPIClient = googleDriveAPIClient;
        }

        private XmlAttributeOverrides AttributeOverrides
        {
            get
            {
                var attrOverrides = new XmlAttributeOverrides();
                var attrs = new XmlAttributes() { XmlIgnore = true };
                attrOverrides.Add(typeof(Model.File), nameof(Model.File.Extension), attrs);

                return attrOverrides;
            }
        }

        public TLibrary ReadLibraryFromStreamAsync<TLibrary>(Stream fileStream)
        {
            fileStream.Position = 0;
            var serializer = new XmlSerializer(typeof(TLibrary), AttributeOverrides);
            var library = (TLibrary)serializer.Deserialize(fileStream);

            if (library is GoogleDriveLibrary googleDriveLibrary)
            {
                googleDriveLibrary.SettingsProvider = _settingsProvider;
                googleDriveLibrary.Client = _googleDriveAPIClient;

                foreach (var image in googleDriveLibrary.Images)
                {
                    ((GoogleDriveImageFile)image).Client = _googleDriveAPIClient;
                    ((GoogleDriveImageFile)image).SettingsProvider = _settingsProvider;
                }
            }

            return library;
        }

        public void WriteLibraryToStreamAsync(Stream fileStream, Library library, bool closeTheStream = true)
        {
            var serializer = new XmlSerializer(library.GetType(), AttributeOverrides);
            serializer.Serialize(fileStream, library);

            if (closeTheStream)
                fileStream.Close();
        }
    }
}
