using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class FileSystemImageFile : ImageFile, IFileSystemEntity
    {
        public string IconSource { get; set; }

        public FileSystemImageFile()
        {
            IconSource = FullPath;
        }
    }
}
