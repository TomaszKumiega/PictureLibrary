using PictureLibrary.FileSystem.API;
using PictureLibrary.Tools.LibraryXml;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.TagService
{
    public class FileSystemTagService
    {
        #region Private fields
        private readonly IFileService _fileService;
        private readonly ILibraryXmlService _libraryXmlService;
        #endregion

        public FileSystemTagService(
            IFileService fileService,
            ILibraryXmlService libraryXmlService)
        {
            _fileService = fileService;
            _libraryXmlService = libraryXmlService;
        }

        #region Public methods
        public async Task AddTagAsync(LocalLibrary library, Tag tag)
        {
            var serializedLibrary = await GetLibraryXml(library);
            var updatedLibraryXml = _libraryXmlService.AddTagNode(serializedLibrary, tag);
            await WriteLibraryXml(library, updatedLibraryXml);
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync(LocalLibrary library)
        {
            var serializedLibrary = await GetLibraryXml(library);
            return _libraryXmlService.GetTags(serializedLibrary);
        }

        public async Task DeleteTagAsync(LocalLibrary library, Tag tag)
        {
            var serializedLibrary = await GetLibraryXml(library);
            var updatedLibraryXml = _libraryXmlService.RemoveTagNode(serializedLibrary, tag);
            await WriteLibraryXml(library, updatedLibraryXml);
        }

        public async Task UpdateTagAsync(LocalLibrary library, Tag tag)
        {
            var serializedLibrary = await GetLibraryXml(library);
            var updatedLibraryXml = _libraryXmlService.UpdateTagNode(serializedLibrary, tag);
            await WriteLibraryXml(library, updatedLibraryXml);
        }
        #endregion

        #region Private methods
        private async Task<string> GetLibraryXml(LocalLibrary library)
        {
            if (library?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(Library));

            using var stream = _fileService.Open(library.FilePath);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private async Task WriteLibraryXml(LocalLibrary library, string xml)
        {
            if (library?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(Library));

            using var stream = _fileService.Open(library.FilePath);
            using var writer = new StreamWriter(stream);
            await writer.WriteAsync(xml);
        }
        #endregion
    }
}
