using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Folder : Directory
    {
        public Folder(string path, string name, FileSystemService fileProvider, Origin origin) : base(path, name, fileProvider, origin)
        {

        }
    }
}
