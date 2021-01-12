using PictureLibraryModel.Services;
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

        public Folder(string path, string name, IFileProvider fileProvider, Origin origin) : base(path, name, fileProvider, origin)
        {

        }
    }
}
