using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Library(string fullPath, string name)
        {
            FullPath = fullPath;
            Name = name;
            Albums = new List<Album>();
        }

        private async Task InitializeAlbumsAsync()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            using (var reader = XmlReader.Create(FullPath, settings))
            {
                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Album")
                        {
                            var albumElement = XNode.ReadFrom(reader) as XElement;

                            var albumName = albumElement.Attribute("name").Value;
                            var imageList = new List<ImageFile>();

                            reader.ReadToDescendant("Image");

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

                            Albums.Add(new Album(albumName, imageList));
                        }
                    }
                }
            }
        }
    }
}
