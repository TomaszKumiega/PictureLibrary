using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LocalLibraryRepository : IRepository<Library>
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IFileService _fileService;

        public static Logger Logger => _logger;

        public LocalLibraryRepository(IFileService fileService)
        {
            _fileService = fileService;
        }

        #region Private methods
        private async Task WriteLibraryToStreamAsync(Stream fileStream, Library library)
        {
            if (fileStream == null) throw new Exception("File creation error");

            // write all owners in one string
            string owners = "";

            if (library.Owners.Count > 0)
            {
                for (int i = 0; i < library.Owners.Count - 1; i++)
                {
                    owners += library.Owners[i].ToString() + ',';
                }

                owners += library.Owners[library.Owners.Count - 1].ToString();
            }

            // create library element
            var libraryElement = new XElement("library", new XAttribute("name", library.Name),
                new XAttribute("description", library.Description), new XAttribute("owners", owners));

            // create tags elements
            var tagsElement = new XElement("tags");

            foreach (var t in library.Tags)
            {
                var tagElement = new XElement("tag", new XAttribute("name", t.Name), new XAttribute("description", t.Description));

                tagsElement.Add(tagElement);
            }

            libraryElement.Add(tagsElement);

            // create images elements
            var imagesElement = new XElement("images");

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
                    new XAttribute("source", i.FullName), new XAttribute("creationTime", i.CreationTime.ToString()), new XAttribute("lastAccessTime", i.LastAccessTime.ToString()),
                    new XAttribute("lastWriteTime", i.LastWriteTime.ToString()), new XAttribute("size", i.Size.ToString()), new XAttribute("tags", tags));

                imagesElement.Add(imageFileElement);
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
        private async Task<Library> ReadLibraryFromStreamAsync(Stream fileStream)
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

                                    if (libraryElement.Attribute("owners").Value.Trim() == String.Empty) break;

                                    foreach (var t in libraryElement.Attribute("owners").Value.Split(','))
                                    {
                                        library.Owners.Add(Guid.Parse(t));
                                    }
                                }
                                break;
                            case "tag":
                                {
                                    var tagElement = await Task.Run(() => XNode.ReadFrom(reader)) as XElement;

                                    var tag = new Tag();
                                    tag.Name = tagElement.Attribute("name").Value;
                                    tag.Description = tagElement.Attribute("description").Value;
                                    tag.Origin = Origin.Local;
                                    tag.ParentLibrary = library;
                                    tag.FullName = "Local\\" + library.Name + "\\" + tag.Name + "\\";

                                    tags.Add(tag);
                                }
                                break;
                            case "imageFile":
                                {
                                    var imageElement = await Task.Run(() => XNode.ReadFrom(reader)) as XElement;

                                    var imageFile = new ImageFile();
                                    imageFile.Name = imageElement.Attribute("name").Value;
                                    imageFile.Extension = ImageExtensionHelper.GetExtension(imageElement.Attribute("extension").Value);
                                    imageFile.FullName = imageElement.Attribute("source").Value;
                                    imageFile.CreationTime = DateTime.Parse(imageElement.Attribute("creationTime").Value);
                                    imageFile.LastAccessTime = DateTime.Parse(imageElement.Attribute("lastAccessTime").Value);
                                    imageFile.LastWriteTime = DateTime.Parse(imageElement.Attribute("lastWriteTime").Value);
                                    imageFile.Size = long.Parse(imageElement.Attribute("size").Value);

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
        #endregion

        public async Task AddAsync(Library library)
        {
            await Task.Run(() => _fileService.Create(library.FullName));
            var fileStream = await Task.Run(() => _fileService.OpenFile(library.FullName));

            await WriteLibraryToStreamAsync(fileStream, library);
        }

        public async Task AddRangeAsync(IEnumerable<Library> libraries)
        {
            foreach (var t in libraries) await AddAsync(t);
        }

        public async Task<IEnumerable<Library>> FindAsync(Predicate<Library> predicate)
        {
            var libraries = await GetAllAsync();

            return libraries.ToList().FindAll(predicate);
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            var filePaths = await Task.Run(() => _fileService.FindFiles("*.plib"));
            var libraries = new List<Library>();

            foreach (var f in filePaths)
            {
                var stream = await Task.Run(() => _fileService.OpenFile(f));
                var library = await ReadLibraryFromStreamAsync(stream);
                library.FullName = f;
                libraries.Add(library);
            }

            return libraries;
        }

        public async Task<Library> GetByPathAsync(string path)
        {
            var stream = await Task.Run(() => _fileService.OpenFile(path));
            var library = await ReadLibraryFromStreamAsync(stream);

            return library;
        }

        public async Task RemoveAsync(string path)
        {
            await Task.Run(() => _fileService.Remove(path));
        }

        public async Task RemoveAsync(Library library)
        {
            await Task.Run(() => _fileService.Remove(library.FullName));
        }

        public async Task RemoveRangeAsync(IEnumerable<Library> libraries)
        {
            foreach(var t in libraries)
            {
                await RemoveAsync(t);
            }
        }

        public async Task UpdateAsync(Library library)
        {
            if (library == null) throw new ArgumentException();
            if (!System.IO.File.Exists(library.FullName)) throw new ArgumentException();

            // Load file for eventual recovery
            XmlDocument document = new XmlDocument();
            await Task.Run(() => document.Load(library.FullName));

            // Remove contents of the file
            string[] text = { "" };
            await Task.Run(() => System.IO.File.WriteAllLines(library.FullName, text));

            try
            {
                // Write library to the file
                var stream = _fileService.OpenFile(library.FullName);
                await WriteLibraryToStreamAsync(stream, library);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                await Task.Run(() => document.Save(library.FullName));
            }
        }
    }
}
