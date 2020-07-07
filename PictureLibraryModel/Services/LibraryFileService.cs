using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryModel.Services
{
    public class LibraryFileService : ILibraryFileService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task<Library> LoadLibraryAsync(string fullPath)
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
                                catch (Exception e)
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

        public Library CreateLibrary(string name, string directory)
        {
            if (!System.IO.Directory.Exists(directory)) throw new DirectoryNotFoundException();
            if (name == null || directory == null) throw new ArgumentNullException();

            var fullPath = directory + '\\' + name + ".plib";

            var libraryElement = new XElement("library", new XAttribute("name", name));

            try
            {
                using (var stream = new FileStream(fullPath, FileMode.CreateNew))
                {
                    var streamWriter = new StreamWriter(stream);
                    var xmlWriter = new XmlTextWriter(streamWriter);

                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 4;

                    libraryElement.Save(xmlWriter);
                }
            }
            catch (IOException e)
            {
                _logger.Debug(e, e.Message);
                throw new Exception("Library already exists");
            }

            return new Library(fullPath, name);
        }


        public async Task<ObservableCollection<Library>> GetAllLibrariesAsync()
        {
            var files = new List<string>();
            
            foreach (var t in System.IO.DriveInfo.GetDrives())
            {
                files.AddRange(Task.Run(() => FindLibrariesInDirectory(t.RootDirectory.ToString())).Result);
            }

            var libraries = new ObservableCollection<Library>();

            foreach (var t in files)
            {
                libraries.Add(LoadLibraryAsync(t).Result);
            }

            return libraries;
        }

        private IEnumerable<string> FindLibrariesInDirectory(string root)
        {
            Queue<string> pending = new Queue<string>();
            pending.Enqueue(root);

            while (pending.Count != 0)
            {
                var path = pending.Dequeue();
                
                List<string> items = null;

                try
                {
                    items = System.IO.Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(s => s.EndsWith("*.plib"))
                        .ToList();
                }
                catch (UnauthorizedAccessException e)
                {
                    _logger.Debug(e, "Unauthorized access exception while looking for libraries");
                }
                catch (Exception e)
                {
                    _logger.Error(e,e.Message);
                }

                if(items !=null && items.Count !=0)
                    foreach (var file in items)
                        yield return file;
                try
                {
                    items = System.IO.Directory.GetDirectories(path).ToList();
                    foreach (var t in items) pending.Enqueue(t);
                }
                catch (Exception e)
                {
                    _logger.Error(e, e.Message);
                }
            }
        }

        public async Task SaveLibrariesAsync(List<Library> libraries)
        {
            if(libraries==null) throw new ArgumentNullException();

            XElement libraryElement;
            foreach (var t in libraries)
            {
                libraryElement = new XElement("library", new XAttribute("name", t.Name));

                foreach (var a in t.Albums)
                {
                    var albumElement = new XElement("album", new XAttribute("name", a));

                    foreach (var i in a.Images)
                    {
                        var imageElement = new XElement("image", new XAttribute("name", i.Name));

                        albumElement.Add(imageElement);
                    }

                    libraryElement.Add(albumElement);
                }

                try
                {
                    using (var stream = new FileStream(t.FullPath, FileMode.Create))
                    {
                        var streamWriter = new StreamWriter(stream);
                        var xmlWriter = new XmlTextWriter(streamWriter);

                        xmlWriter.Formatting = Formatting.Indented;
                        xmlWriter.Indentation = 4;

                        libraryElement.Save(xmlWriter);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e, e.Message);
                }
            }
        }
    }
}
