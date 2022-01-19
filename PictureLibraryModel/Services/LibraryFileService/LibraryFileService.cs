﻿using NLog;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Services.LibraryFileService
{
    public class LibraryFileService : ILibraryFileService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private Func<LocalImageFile> LocalImageFileLocator { get; }

        public LibraryFileService(Func<LocalImageFile> localImageFileLocator)
        {
            LocalImageFileLocator = localImageFileLocator;
        }

        public async Task<Library> ReadLibraryFromStreamAsync(Stream fileStream, Guid? remoteStorageId)
        {
            var tags = new List<Tag>();
            var images = new List<ImageFile>();
            var library = new Library();

            if (fileStream.Length == 0) throw new ArgumentException("Given stream is empty");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            using (var reader = XmlReader.Create(fileStream, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "library":
                                {
                                    var libraryElement = await Task.Run(() => XNode.ReadFrom(reader)) as XElement;

                                    library.Name = libraryElement.Attribute("name").Value;
                                    library.Description = libraryElement.Attribute("description").Value;
                                    library.RemoteStorageInfoId = remoteStorageId;

                                    if (fileStream is FileStream) library.Path = (fileStream as FileStream).Name;
                                }
                                break;
                            case "tag":
                                {
                                    var tagElement = await Task.Run(() => XNode.ReadFrom(reader)) as XElement;

                                    var tag = new Tag();
                                    tag.Name = tagElement.Attribute("name").Value;
                                    tag.Description = tagElement.Attribute("description").Value;
                                    tag.RemoteStorageInfoId = remoteStorageId;
                                    tag.Path = "Local\\" + library.Name + "\\" + tag.Name + "\\";
                                    tag.Color = tagElement.Attribute("color").Value;
                                    tags.Add(tag);
                                }
                                break;
                            case "imageFile":
                                {
                                    var imageElement = await Task.Run(() => XNode.ReadFrom(reader)) as XElement;

                                    var imageFile = LocalImageFileLocator();
                                    imageFile.Name = imageElement.Attribute("name").Value;
                                    imageFile.Extension = ImageExtensionHelper.GetExtension(imageElement.Attribute("extension").Value);
                                    imageFile.Path = imageElement.Attribute("source").Value;

                                    foreach (var t in imageElement.Attribute("tags").Value.Split(','))
                                    {
                                        imageFile.Tags.Add(tags.Find(x => x.Name == t));
                                    }

                                    images.Add(imageFile);
                                }
                                break;
                        }
                    }
                }
            }

            library.Tags = tags;
            library.Images = images;

            return library;
        }
        public async Task WriteLibraryToStreamAsync(Stream fileStream, Library library)
        {
            if (fileStream == null) throw new Exception("File creation error");

            // create library element
            var libraryElement = new XElement("library", new XAttribute("name", library.Name),
                new XAttribute("description", library.Description));

            // create tags elements
           
            var tagsElement = new XElement("tags");

            if (library.Tags != null)
            {
                foreach (var t in library.Tags)
                {
                    var tagElement = new XElement("tag", new XAttribute("name", t.Name), new XAttribute("description", t.Description), new XAttribute("color", t.Color));

                    tagsElement.Add(tagElement);
                }
            }

            libraryElement.Add(tagsElement);

            // create images elements
            var imagesElement = new XElement("images");

            if(library.Images != null)
            {
                foreach (var i in library.Images)
                {
                    // write all tags to one string
                    string tags = "";

                    for (int it = 0; it < i.Tags.Count - 1; it++)
                    {
                        tags += i.Tags[it].Name + ',';
                    }

                    tags += i.Tags[i.Tags.Count - 1].Name;


                    var imageFileElement = new XElement("imageFile", new XAttribute("name", i.Name), new XAttribute("extension", i.Extension),
                        new XAttribute("source", i.Path), new XAttribute("tags", tags));

                    imagesElement.Add(imageFileElement);
                }
            }

            libraryElement.Add(imagesElement);

            // save elements to the file
            try
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var xmlWriter = new XmlTextWriter(streamWriter);

                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 4;

                    await Task.Run(() => libraryElement.Save(xmlWriter));
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
            }
        }
    }
}
