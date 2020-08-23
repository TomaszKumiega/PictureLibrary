using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Services
{
    public class LibraryFileService : ILibraryFileService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IFileSystemService _fileSystem;

        public LibraryFileService(IFileSystemService fileSystem)
        {
            _fileSystem = fileSystem;
        }

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

            var libraryElement = new XElement("library");

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


        public async Task<List<Library>> GetAllLibrariesAsync()
        {
            var drives = _fileSystem.GetDrives();
            var libraries = new List<Library>();
            
            foreach(var t in drives)
            {
                libraries.AddRange(Task.Run(() => FindLibraries(t)).Result);
            }

            return libraries;
        }

        public IEnumerable<Library> FindLibraries(IFileSystemEntity directory)
        {
            var files = new List<string>();
            var libraries = new List<Library>();

            foreach (var t in (directory as Model.Directory).Children)
            {
                if((t as IFileSystemEntity).Name.EndsWith(".plib"))
                {
                    files.Add((t as IFileSystemEntity).FullPath);
                }
            }

            foreach(var t in files)
            {
                libraries.Add(new Library(t, Path.GetFileNameWithoutExtension(t)));
            }

            foreach(var t in libraries)
            {
                yield return t;
            }

            
            foreach (Model.Directory t in (directory as Model.Directory).Children)
            {
                foreach(var i in FindLibraries(t))
                {
                    yield return i;
                }
            }
        }

        public void SaveLibraries(List<Library> libraries)
        {
            throw new NotImplementedException();
        }
    }
}
