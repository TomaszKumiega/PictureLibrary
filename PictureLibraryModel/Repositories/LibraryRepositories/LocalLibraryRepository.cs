using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.SettingsProvider;
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
        private IDirectoryService _directoryService;
        private ISettingsProviderService _settingsProvider;
        private ILibraryFileService _libraryFileService;

        public static Logger Logger => _logger;

        public LocalLibraryRepository(IFileService fileService, IDirectoryService directoryService, 
            ISettingsProviderService settingsProvider, ILibraryFileService libraryFileService)
        {
            _fileService = fileService;
            _directoryService = directoryService;
            _settingsProvider = settingsProvider;
            _libraryFileService = libraryFileService;
        }

        public async Task AddAsync(Library library)
        {
            await Task.Run(() => _fileService.Create(library.FullName));
            var fileStream = await Task.Run(() => _fileService.OpenFile(library.FullName));

            _settingsProvider.Settings.ImportedLibraries.Add(library.FullName);
            await _settingsProvider.SaveSettingsAsync();

            await _libraryFileService.WriteLibraryToStreamAsync(fileStream, library);
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
            if (_settingsProvider.Settings.ImportedLibraries == null) throw new Exception("Error loading libraries. Imported libraries are null.");

            var libraries = new List<Library>();

            foreach (var t in _settingsProvider.Settings.ImportedLibraries)
            {
                try
                {
                    var stream = await Task.Run(() => _fileService.OpenFile(t));
                    var library = await _libraryFileService.ReadLibraryFromStreamAsync(stream);
                    library.FullName = t;
                    libraries.Add(library);
                }
                catch(FileNotFoundException e)
                {
                    _logger.Debug(e, "Library file not found.");
                }
                catch(Exception e)
                {
                    _logger.Debug(e, "Couldnt read " + t);
                }
            }

            return libraries;
        }

        public async Task<Library> GetByPathAsync(string path)
        {
            var stream = await Task.Run(() => _fileService.OpenFile(path));
            var library = await _libraryFileService.ReadLibraryFromStreamAsync(stream);

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
                await _libraryFileService.WriteLibraryToStreamAsync(stream, library);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                await Task.Run(() => document.Save(library.FullName));
            }
        }
    }
}
