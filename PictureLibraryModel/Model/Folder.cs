using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Folder : Directory
    {
        public Folder()
        {

        }

        public Folder(string path, string name, IDirectoryService directoryService, Origin origin) : base(path, name, directoryService, origin)
        {

        }
    }
}
