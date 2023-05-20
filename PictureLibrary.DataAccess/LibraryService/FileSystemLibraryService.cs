using PictureLibrary.AppSettings;
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
        private readonly ILibraryXmlService _libraryXmlEditor;
        private readonly ILibrarySettingsProvider _librarySettingsProvider;
        #endregion

        public FileSystemLibraryService(
            IFileService fileService,
            IXmlSerializer xmlSerializer,
            IDirectoryService directoryService,
            ILibraryXmlService libraryXmlEditor,
            ILibrarySettingsProvider librarySettingsProvider)
        {
            _fileService = fileService;
            _xmlSerializer = xmlSerializer;
            _libraryXmlEditor = libraryXmlEditor;
            _directoryService = directoryService;
            _librarySettingsProvider = librarySettingsProvider;
        }

        #region Public methods
        public async Task<LocalLibrary> AddLibraryAsync(LocalLibrary library)
        {
            var serializedLibrary = await SerializeLibraryAsync(library);
            var path = await CreateLibraryDirectoriesAsync(library) + Path.PathSeparator + $"{library.Name}.plib";

            await WriteLibraryToFileAsync(serializedLibrary, path);

            library.FilePath = path;

            return library;
        }

        public async Task<bool> DeleteLibraryAsync(LocalLibrary library)
        {
            if (library?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(library));

            return await Task.Run(() => _fileService.Delete(library.FilePath));
        }

        public async Task<IEnumerable<LocalLibrary>> GetAllLibrariesAsync()
        {
            DirectoryInfo[] subDirectories = await GetSubdirectoriesOfLibrariesMainFolderAsync();

            return await GetLibrariesFromSubdirectoriesAsync(subDirectories);
        }

        public async Task UpdateLibraryAsync(LocalLibrary library)
        {
            if (library?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(library));

            string serializedLibrary;
            using (Stream libraryFileStream = _fileService.Open(library.FilePath))
            using (StreamReader sr = new(libraryFileStream))
            {
                serializedLibrary = await sr.ReadToEndAsync();
            }

            string updatedSerializedLibrary = _libraryXmlEditor.UpdateLibraryNode(serializedLibrary, library);

            using (Stream updatedLibraryFileStream = _fileService.Open(library.FilePath))
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
            string? librariesDirectory = _librarySettingsProvider.GetSettings()?.LocalLibrariesStoragePath;

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
                FileInfo? libraryFileInfo = subDirectory.GetFiles().FirstOrDefault();

                if (libraryFileInfo != null)
                {
                    LocalLibrary? library = await ReadLibraryFileAsync(libraryFileInfo);

                    if (library != null)
                    {
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

            var directoryPath = librarySettings.LocalLibrariesStoragePath + Path.PathSeparator + library.Name;

            await Task.Run(() => _directoryService.Create(directoryPath));

            var imageDirectoryPath = directoryPath + Path.PathSeparator + "Images";

            await Task.Run(() => _directoryService.Create(imageDirectoryPath));

            return directoryPath;
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
