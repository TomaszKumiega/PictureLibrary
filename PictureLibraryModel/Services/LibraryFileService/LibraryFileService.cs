﻿using PictureLibraryModel.Model;
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

        public TLibrary ReadLibraryFromStreamAsync<TLibrary>(Stream fileStream)
        {
            var serializer = new XmlSerializer(typeof(TLibrary), AttributeOverrides);
            var library = (TLibrary)serializer.Deserialize(fileStream);

            return library;
        }

        public void WriteLibraryToStreamAsync(Stream fileStream, Library library)
        {
            var serializer = new XmlSerializer(library.GetType(), AttributeOverrides);
            serializer.Serialize(fileStream, library);

            fileStream.Close();
        }

        public Library ReloadLibrary(Library library)
        {
            var fileStream = System.IO.File.Open(library.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            
            var updatedLibrary = ReadLibraryFromStreamAsync<LocalLibrary>(fileStream);
            
            fileStream.Close();

            return updatedLibrary;
        }
    }
}
