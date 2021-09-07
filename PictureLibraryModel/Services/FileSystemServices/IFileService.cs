using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public interface IFileService : IFileSystemService
    {
        Stream OpenFile(string path, FileMode fileMode);
        Stream OpenFile(string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare);
        Task<IEnumerable<string>> FindFilesAsync(string searchPattern, string directory);
        byte[] ReadAllBytes(string path);
        void WriteAllBytes(string path, byte[] bytes);
        void WriteAllLines(string path, string[] text);
    }
}
