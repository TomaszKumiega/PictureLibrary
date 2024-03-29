﻿using PictureLibrary.AppSettings;
using PictureLibrary.FileSystem.API;
using PictureLibrary.FileSystem.API.Directories;
using PictureLibrary.Tools.LibraryXml;
using PictureLibrary.Tools.XamlSerializer;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.LibraryService
{
    public class FileSystemLibraryService : IFileSystemLibraryService
    {
        #region Private fields
        private readonly IFileService _fileService;
        private readonly IXmlSerializer _xmlSerializer;
        private readonly IDirectoryService _directoryService;
        private readonly ILibraryXmlService<LocalLibrary> _libraryXmlEditor;
        private readonly ILibrarySettingsProvider _librarySettingsProvider;
        #endregion

        public FileSystemLibraryService(
            IFileService fileService,
            IXmlSerializer xmlSerializer,
            IDirectoryService directoryService,
            ILibraryXmlService<LocalLibrary> libraryXmlEditor,
            ILibrarySettingsProvider librarySettingsProvider)
        {
            _fileService = fileService;
            _xmlSerializer = xmlSerializer;
            _libraryXmlEditor = libraryXmlEditor;
            _directoryService = directoryService;
            _librarySettingsProvider = librarySettingsProvider;
        }

        #region Public methods
        public async Task<Library> AddLibraryAsync(Library library)
        {
            if (library is not LocalLibrary localLibrary)
                throw new ArgumentException($"Library must be a type of {nameof(LocalLibrary)}", nameof(library));
            
            localLibrary.Id = Guid.NewGuid();
            var serializedLibrary = await SerializeLibraryAsync(localLibrary);
            var path = await CreateLibraryDirectoriesAsync(localLibrary) + Path.DirectorySeparatorChar + $"{localLibrary.Name}.plib";
            localLibrary.FilePath = path;

            await WriteLibraryToFileAsync(serializedLibrary, path);

            return localLibrary;
        }

        public async Task<bool> DeleteLibraryAsync(Library library)
        {
            if (library is not LocalLibrary localLibrary)
                throw new ArgumentException($"Library must be a type of {nameof(LocalLibrary)}", nameof(library));

            if (localLibrary?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(localLibrary));

            var libraryDirectory = _fileService.GetFileInfo(localLibrary.FilePath).DirectoryName;

            bool success = await Task.Run(() => _fileService.Delete(localLibrary.FilePath));

            if (libraryDirectory != null)
            {
                _directoryService.DeleteDirectory(libraryDirectory);
            }

            return success;
        }

        public async Task<IEnumerable<LocalLibrary>> GetAllLibrariesAsync()
        {
            DirectoryInfo[] subDirectories = await GetSubdirectoriesOfLibrariesMainFolderAsync();

            return await GetLibrariesFromSubdirectoriesAsync(subDirectories);
        }

        public async Task UpdateLibraryAsync(Library library)
        {
            if (library is not LocalLibrary localLibrary)
                throw new ArgumentException($"Library must be a type of {nameof(LocalLibrary)}", nameof(library));

            if (localLibrary?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(localLibrary));

            string serializedLibrary;
            using (Stream libraryFileStream = _fileService.Open(localLibrary.FilePath))
            using (StreamReader sr = new(libraryFileStream))
            {
                serializedLibrary = await sr.ReadToEndAsync();
            }

            string updatedSerializedLibrary = _libraryXmlEditor.UpdateLibraryNode(serializedLibrary, localLibrary);

            using (Stream updatedLibraryFileStream = _fileService.Open(localLibrary.FilePath))
            using (StreamWriter sr = new(updatedLibraryFileStream))
            {
                await sr.WriteAsync(updatedSerializedLibrary);
            }
        }
        #endregion

        #region Private methods
        private async Task<string> SerializeLibraryAsync(LocalLibrary library)
            => await Task.Run(() => _xmlSerializer.SerializeToString(library));

        private async Task<DirectoryInfo[]> GetSubdirectoriesOfLibrariesMainFolderAsync()
        {
            var librarySettings = _librarySettingsProvider.GetSettings();

            CreateLibrariesDirectoryIfItDoesntExist(librarySettings);

            string? librariesDirectory = librarySettings.LocalLibrariesStoragePath;

            if (librariesDirectory == null)
                return Array.Empty<DirectoryInfo>();

            DirectoryInfo mainDirectoryInfo = _directoryService.GetDirectoryInfo(librariesDirectory);

            return await Task.Run(mainDirectoryInfo.GetDirectories);
        }

        private async Task<IEnumerable<LocalLibrary>> GetLibrariesFromSubdirectoriesAsync(DirectoryInfo[] subDirectories)
        {
            var libraries = new List<LocalLibrary>();

            foreach (var subDirectory in subDirectories)
            {
                FileInfo? libraryFileInfo = subDirectory.GetFiles().FirstOrDefault(x => x.Extension == ".plib");

                if (libraryFileInfo != null)
                {
                    LocalLibrary? library = await ReadLibraryFileAsync(libraryFileInfo);

                    if (library != null)
                    {
                        library.FilePath = libraryFileInfo.FullName;
                        libraries.Add(library);
                    }
                }
            }

            return libraries;
        }

        private async Task<LocalLibrary?> ReadLibraryFileAsync(FileInfo fileInfo)
        {
            using var libraryFileStream = _fileService.Open(fileInfo.FullName);
            using var streamReader = new StreamReader(libraryFileStream);

            string serializedLibrary = await streamReader.ReadToEndAsync();

            return await Task.Run(() => _xmlSerializer.DeserializeFromString<LocalLibrary>(serializedLibrary));
        }

        private async Task<string> CreateLibraryDirectoriesAsync(LocalLibrary library)
        {
            var librarySettings = await Task.Run(_librarySettingsProvider.GetSettings);

            CreateLibrariesDirectoryIfItDoesntExist(librarySettings);

            var directoryPath = librarySettings.LocalLibrariesStoragePath + Path.DirectorySeparatorChar + library.Name;

            await Task.Run(() => _directoryService.Create(directoryPath));

            var imageDirectoryPath = directoryPath + Path.DirectorySeparatorChar + "Images";

            await Task.Run(() => _directoryService.Create(imageDirectoryPath));

            return directoryPath;
        }

        private void CreateLibrariesDirectoryIfItDoesntExist(LibrariesSettings settings)
        {
            if (!_fileService.Exists(settings.LocalLibrariesStoragePath))
            {
                _directoryService.Create(settings.LocalLibrariesStoragePath);
            }
        }

        private async Task WriteLibraryToFileAsync(string serializedLibrary, string path)
        {
            using var stream = _fileService.Create(path);
            using var streamWriter = new StreamWriter(stream);

            await streamWriter.WriteAsync(serializedLibrary);
        }
        #endregion
    }
}
