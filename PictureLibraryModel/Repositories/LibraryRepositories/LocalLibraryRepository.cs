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
        private IFileService FileService { get; }
        private ISettingsProviderService SettingsProvider { get; }
        private ILibraryFileService LibraryFileService { get; }

        public LocalLibraryRepository(IFileService fileService, ISettingsProviderService settingsProvider, ILibraryFileService libraryFileService)
        {
            FileService = fileService;
            SettingsProvider = settingsProvider;
            LibraryFileService = libraryFileService;
        }

        public async Task AddAsync(Library library)
        {
            await Task.Run(() => FileService.Create(library.FullName));
            var fileStream = await Task.Run(() => FileService.OpenFile(library.FullName));

            SettingsProvider.Settings.ImportedLibraries.Add(library.FullName);
            await SettingsProvider.SaveSettingsAsync();

            await LibraryFileService.WriteLibraryToStreamAsync(fileStream, library);
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
            var libraries = new List<Library>();

            if (SettingsProvider.Settings.ImportedLibraries == null) return libraries;

            foreach (var t in SettingsProvider.Settings.ImportedLibraries)
            {
                try
                {
                    var stream = await Task.Run(() => FileService.OpenFile(t));
                    var library = await LibraryFileService.ReadLibraryFromStreamAsync(stream);
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
            var stream = await Task.Run(() => FileService.OpenFile(path));
            var library = await LibraryFileService.ReadLibraryFromStreamAsync(stream);

            return library;
        }

        public async Task RemoveAsync(string path)
        {
            await Task.Run(() => FileService.Remove(path));
        }

        public async Task RemoveAsync(Library library)
        {
            await Task.Run(() => FileService.Remove(library.FullName));
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
            if (library == null) throw new ArgumentNullException();
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
                var stream = FileService.OpenFile(library.FullName);
                await LibraryFileService.WriteLibraryToStreamAsync(stream, library);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                await Task.Run(() => document.Save(library.FullName));
            }
        }
    }
}
