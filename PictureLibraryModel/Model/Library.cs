using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Model
{
    public class Library
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public string FullPath { get; }
        public string Name { get; }
        public List<Album> Albums { get; }

        private Library()
        {

        }

        private Library(string fullPath, string name, List<Album> albums)
        {
            FullPath = fullPath;
            Name = name;
            Albums = albums;
        }

        public static async Task<Library> LoadLibraryAsync(string fullPath)
        {
            var albumsList = new List<Album>();
            string libraryName = null;

            if (!File.Exists(fullPath)) throw new FileNotFoundException(); 

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            using (var reader = XmlReader.Create(fullPath, settings))
            {
                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "album")
                        {
                            var albumElement = XNode.ReadFrom(reader) as XElement;

                            var albumName = albumElement.Attribute("name").Value;
                            var imageList = new List<ImageFile>();

                            reader.ReadToDescendant("image");

                            do
                            {
                                var imageElement = XNode.ReadFrom(reader) as XElement;

                                try
                                {
                                    var image = new ImageFile(imageElement.Attribute("path").Value);
                                    imageList.Add(image);
                                }
                                catch(Exception e)
                                {
                                    _logger.Debug(e, e.Message);
                                }

                            } while (reader.ReadToNextSibling("image"));

                            albumsList.Add(new Album(albumName, imageList));
                        }

                        if (reader.Name == "library")
                        {
                            var libraryElement = XNode.ReadFrom(reader) as XElement;

                            libraryName = libraryElement.Attribute("name").Value;
                        }
                    }
                }
            }

            return new Library(fullPath, libraryName, albumsList);
        }
    }
}
