using PictureLibraryModel.Model;
using System.Collections.Generic;
using System.IO;
using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryModel.Services
{
    public interface IFileProvider
    {
        IEnumerable<Folder> GetSubFolders(string topDirectory, SearchOption option);
        IEnumerable<Directory> GetRootDirectories();
    }
}
