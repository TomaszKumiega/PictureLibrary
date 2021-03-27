using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public interface IFileService : IFileSystemService
    {
        Stream OpenFile(string path);
        Task<IEnumerable<string>> FindFilesAsync(string searchPattern, string directory);
        byte[] ReadAllBytes(string path);
    }
}
