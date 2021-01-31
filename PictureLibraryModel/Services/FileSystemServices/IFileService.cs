using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public interface IFileService : IFileSystemService
    {
        Stream OpenFile(string path);
    }
}
