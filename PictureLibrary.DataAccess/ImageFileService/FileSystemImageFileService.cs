using PictureLibrary.FileSystem.API;
using PictureLibrary.Tools.LibraryXml;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public class FileSystemImageFileService : IImageFileService
    {
        #region Private fields
        private readonly IFileService _fileService;
        private readonly ILibraryXmlService<LocalLibrary> _libraryXmlService;
        #endregion

        public FileSystemImageFileService(
            IFileService fileService,
            ILibraryXmlService<LocalLibrary> libraryXmlService)
        {
            _fileService = fileService;
            _libraryXmlService = libraryXmlService;
        }

        #region Public methods
        public async Task AddImageFile(ImageFile imageFile, Stream imageFileContent, Library library)
        {
            if (imageFile is not LocalImageFile localImageFile)
                throw new ArgumentException("Invalid image file type.", nameof(imageFile));

            if (library is not LocalLibrary localLibrary
                || localLibrary?.FilePath == null)
            {
                throw new ArgumentException("Invalid library type.", nameof(localLibrary));
            }

            string libraryXml = await GetLibraryXmlAsync(localLibrary);
            string directoryName = _fileService.GetFileInfo(localLibrary.FilePath).DirectoryName ?? throw new ArgumentException("Invalid file path.", nameof(localLibrary));
            
            string path = directoryName + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + $"{localImageFile.Name}.{localImageFile.Extension}";

            using var stream = _fileService.Create(path);
            await imageFileContent.CopyToAsync(stream);

            string updatedLibraryXml = _libraryXmlService.AddImageFileNode(libraryXml, localImageFile);

            await WriteLibraryXmlAsync(localLibrary, updatedLibraryXml);
        }

        public async Task<IEnumerable<ImageFile>> GetAllImageFiles(Library library)
        {
            if (library is not LocalLibrary localLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            string libraryXml = await GetLibraryXmlAsync(localLibrary);

            return _libraryXmlService.GetImageFiles<LocalImageFile>(libraryXml);
        }

        public async Task DeleteImageFile(ImageFile imageFile, Library library)
        {
            if (imageFile is not LocalImageFile localImageFile
                || localImageFile?.Path == null)
                throw new ArgumentException("Invalid image file.", nameof(imageFile));

            if (library is not LocalLibrary localLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            string libraryXml = await GetLibraryXmlAsync(localLibrary);

            string updatedLibraryXml = _libraryXmlService.RemoveImageFileNode(libraryXml, localImageFile);

            await WriteLibraryXmlAsync(localLibrary, updatedLibraryXml);

            await Task.Run(() => _fileService.Delete(localImageFile.Path));
        }

        public async Task UpdateImageFile(ImageFile imageFile, Library library)
        {
            if (imageFile is not LocalImageFile localImageFile)
                throw new ArgumentException("Invalid image file type.", nameof(imageFile));

            if (library is not LocalLibrary localLibrary)
                throw new ArgumentException("Invalid library type.", nameof(library));

            string libraryXml = await GetLibraryXmlAsync(localLibrary);

            string updatedLibraryXml = _libraryXmlService.UpdateImageFileNode(libraryXml, localImageFile);

            await WriteLibraryXmlAsync(localLibrary, updatedLibraryXml);
        }

        public async Task<Stream> GetFileContent(ImageFile imageFile)
        {
            if (imageFile is not LocalImageFile localImageFile
                || localImageFile?.Path == null)
            {
                throw new ArgumentException(string.Empty, nameof(imageFile));
            }

            return await Task.Run(() => _fileService.Open(localImageFile.Path));
        }

        public async Task UpdateFileContent(ImageFile imageFile, Stream newFileContentStream)
        {
            if (imageFile is not LocalImageFile localImageFile
                || localImageFile?.Path == null)
            {
                throw new ArgumentException("Invalid image file.", nameof(localImageFile));
            }

            using var stream = _fileService.Open(localImageFile.Path);
            await newFileContentStream.CopyToAsync(stream);
        }
        #endregion

        #region Private methods
        private async Task<string> GetLibraryXmlAsync(LocalLibrary library)
        {
            if (library?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(library));

            using var stream = _fileService.Open(library.FilePath);
            using StreamReader sr = new(stream);
            
            return await sr.ReadToEndAsync();
        }

        private async Task WriteLibraryXmlAsync(LocalLibrary library, string xml)
        {
            if (library?.FilePath == null)
                throw new ArgumentException(string.Empty, nameof(library));

            using var stream = _fileService.Open(library.FilePath);
            using StreamWriter sw = new(stream);
            await sw.WriteAsync(xml);
        }
        #endregion
    }
}
