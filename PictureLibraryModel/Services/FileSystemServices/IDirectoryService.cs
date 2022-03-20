using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
