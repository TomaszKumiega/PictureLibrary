using PictureLibraryModel.Model;
using System.Collections.Generic;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public interface IDirectoryService : IFileSystemService
    {
        IEnumerable<Folder> GetSubFolders(string path);
        IEnumerable<Directory> GetRootDirectories();
        IEnumerable<IExplorableElement> GetDirectoryContent(string path);
        bool IsDirectory(string path);
        Directory GetParent(string path);
    }
}
