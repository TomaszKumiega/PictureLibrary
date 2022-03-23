using PictureLibraryModel.Model;
using System;
using System.IO;
using System.Xml.Serialization;

namespace PictureLibraryModel.Services.LibraryFileService
{
    public class LibraryFileService : ILibraryFileService
    {
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

        public Library ReadLibraryFromStreamAsync(Stream fileStream, Guid? remoteStorageId)
        {
            var serializer = new XmlSerializer(typeof(Library), AttributeOverrides);
            var library = (Library)serializer.Deserialize(fileStream);
            library.RemoteStorageInfoId = remoteStorageId;

            return library;
        }

        public void WriteLibraryToStreamAsync(Stream fileStream, Library library)
        {
            var serializer = new XmlSerializer(typeof(Library), AttributeOverrides);
            serializer.Serialize(fileStream, library);

            fileStream.Close();
        }

        public Library ReloadLibrary(Library library)
        {
            var fileStream = System.IO.File.Open(library.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            
            var updatedLibrary = ReadLibraryFromStreamAsync(fileStream, library.RemoteStorageInfoId);
            
            fileStream.Close();

            return updatedLibrary;
        }
    }
}
