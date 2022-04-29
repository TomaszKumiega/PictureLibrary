using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PictureLibraryModel.DataProviders
{
    public class LocalLibraryProvider : ILibraryProvider
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly ILibraryFileService _libraryFileService;

        public LocalLibraryProvider(
            IFileService fileService, 
            IDirectoryService directoryService, 
            ISettingsProvider settingsProvider, 
            ILibraryFileService libraryFileService)
        {
            _fileService = fileService;
            _directoryService = directoryService;
            _settingsProvider = settingsProvider;
            _libraryFileService = libraryFileService;
        }

        public void AddLibraries(IEnumerable<Library> libraries)
        {
            if (libraries == null)
                throw new ArgumentNullException(nameof(libraries));
            foreach (var library in libraries)
            {
                AddLibrary(library);
            }
        }

        public void AddLibrary(Library library)
        {
            if (library == null)
                throw new ArgumentNullException(nameof(library));

            if (library.Path == null)
                throw new ArgumentException(nameof(library.Path));

            var directory = _directoryService.GetParent(library.Path);
            
            _directoryService.Create(directory.Path + "\\Images");
            _fileService.Create(library.Path);

            var fileStream = _fileService.OpenFile(library.Path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);

            _settingsProvider.Settings.ImportedLocalLibraries.Add(library.Path);
            _settingsProvider.SaveSettings();

            _libraryFileService.WriteLibraryToStreamAsync(fileStream, library);
        }

        public IEnumerable<Library> GetAllLibraries()
        {
            var libraries = new List<Library>();

            if (_settingsProvider.Settings.ImportedLocalLibraries == null) return libraries;
            var importedLibraries = _settingsProvider.Settings.ImportedLocalLibraries.ToArray();

            foreach (var t in importedLibraries)
            {
                Stream stream = null;

                try
                {
                    stream = _fileService.OpenFile(t, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var library = _libraryFileService.ReadLibraryFromStreamAsync(stream, null);
                    libraries.Add(library);
                }
                catch (FileNotFoundException e)
                {
                    _logger.Debug(e, "Library file not found.");
                    _settingsProvider.Settings.ImportedLocalLibraries.Remove(t);
                    _settingsProvider.SaveSettings();
                    _logger.Info("Removed library entry: " + t + " from settings");
                }
                catch (Exception e)
                {
                    _logger.Debug(e, "Couldn't read " + t);
                }
                finally
                {
                    stream.Close();
                }
            }

            return libraries;
        }

        public void RemoveLibrary(Library library)
        {
            if (library == null) 
                throw new ArgumentNullException(nameof(library));

            _fileService.Remove(library.Path);
            _settingsProvider.Settings.ImportedLocalLibraries.Remove(library.Path);
            _settingsProvider.SaveSettings();
        }

        public void UpdateLibrary(Library library)
        {
            if (library == null) 
                throw new ArgumentNullException(nameof(library));
            if (!_fileService.Exists(library.Path)) 
                throw new ArgumentException(nameof(library));

            // load file for potential recovery
            XmlDocument document = new XmlDocument();
            document.Load(library.Path);

            // remove contents of the file
            _fileService.Create(library.Path);

            Stream stream = null;

            try
            {
                // write updated library to the file
                stream = _fileService.OpenFile(library.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                _libraryFileService.WriteLibraryToStreamAsync(stream, library);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Library update error:" + e.Message);
                document.Save(library.Path);
            }
            finally
            {
                stream.Close();
            }
        }    
    }
}
