using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.LibraryFileService
{
    public interface ILibraryFileService
    {
        Task WriteLibraryToStreamAsync(Stream fileStream, Library library);
        Task<Library> ReadLibraryFromStreamAsync(Stream fileStream);
    }
}
