using PictureLibrary.FileSystem.API;
using PictureLibrary.Tools.LibraryXml;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public class FileSystemImageFileService
    {
        private readonly IFileService _fileService;
        private readonly ILibraryXmlService _libraryXmlService;

        public FileSystemImageFileService(
            IFileService fileService,
            ILibraryXmlService libraryXmlService)
        {
            _fileService = fileService;
            _libraryXmlService = libraryXmlService;
        }

        public async Task AddImageFileAsync(LocalImageFile localImageFile, Stream imageFileContent, LocalLibrary library)
        {
            if (library?.FilePath == null) 
                throw new ArgumentException(string.Empty, nameof(library));

            string libraryXml = await GetLibraryXmlAsync(library);
            string path = _fileService.GetFileInfo(library.FilePath).DirectoryName! + Path.PathSeparator + "Images" + Path.PathSeparator + $"{localImageFile.Name}.{localImageFile.Extension}";

            using var stream = _fileService.Create(path);
            await imageFileContent.CopyToAsync(stream);

            string updatedLibraryXml = _libraryXmlService.AddImageFileNode(libraryXml, localImageFile);

            await WriteLibraryXmlAsync(library, updatedLibraryXml);
        }

        public async Task<IEnumerable<LocalImageFile>> GetAllImageFilesAsync(LocalLibrary library)
        {
            string libraryXml = await GetLibraryXmlAsync(library);

            return _libraryXmlService.GetImageFiles<LocalImageFile>(libraryXml);
        }

        public async Task DeleteImageFile(LocalImageFile imageFile, LocalLibrary library)
        {
            string libraryXml = await GetLibraryXmlAsync(library);

            string updatedLibraryXml = _libraryXmlService.RemoveImageFileNode(libraryXml, imageFile);

            await WriteLibraryXmlAsync(library, updatedLibraryXml);
        }

        public async Task UpdateImageFile(LocalImageFile imageFile, LocalLibrary library)
        {
            string libraryXml = await GetLibraryXmlAsync(library);

            string updatedLibraryXml = _libraryXmlService.UpdateImageFileNode(libraryXml, imageFile);

            await WriteLibraryXmlAsync(library, updatedLibraryXml);
        }

        public Stream GetFileContentStream(LocalImageFile imageFile)
        {
            if (imageFile?.Path == null)
                throw new ArgumentException(string.Empty, nameof(imageFile));

            return _fileService.Open(imageFile.Path);
        }

        public async Task UpdateFileContent(LocalImageFile imageFile, Stream newFileContentStream)
        {
            if (imageFile?.Path == null)
                throw new ArgumentException(string.Empty, nameof(imageFile));

            using var stream = _fileService.Open(imageFile.Path);
            await newFileContentStream.CopyToAsync(stream);
        }

        public bool DeleteFile(LocalImageFile imageFile)
        {
            if (imageFile?.Path == null)
                throw new ArgumentException(string.Empty, nameof(imageFile));

            return _fileService.Delete(imageFile.Path);
        }

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
    }
}
