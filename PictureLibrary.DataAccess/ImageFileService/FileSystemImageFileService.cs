﻿using PictureLibrary.FileSystem.API;
using PictureLibrary.Tools.XamlEditor;
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
            return _fileService.Open(imageFile.Path);
        }

        public async Task UpdateFileContent(LocalImageFile imageFile, Stream newFileContentStream)
        {
            using var stream = _fileService.Open(imageFile.Path);
            await newFileContentStream.CopyToAsync(stream);
        }

        public bool RemoveFile(LocalImageFile imageFile)
        {
            return _fileService.Delete(imageFile.Path);
        }

        private async Task<string> GetLibraryXmlAsync(LocalLibrary library)
        {
            using var stream = _fileService.Open(library.FilePath);
            using StreamReader sr = new(stream);
            
            return await sr.ReadToEndAsync();
        }

        private async Task WriteLibraryXmlAsync(LocalLibrary library, string xml)
        {
            using var stream = _fileService.Open(library.FilePath);
            using StreamWriter sw = new(stream);
            await sw.WriteAsync(xml);
        }
    }
}
