using PictureLibraryModel.Model;
using System;
using System.IO;

namespace PictureLibraryModel.Services.LibraryFileService
{
    public interface ILibraryFileService
    {
        void WriteLibraryToStreamAsync(Stream fileStream, Library library, bool closeTheStream = true);
        TLibrary ReadLibraryFromStreamAsync<TLibrary>(Stream fileStream);
    }
}
