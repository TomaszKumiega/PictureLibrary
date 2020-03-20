using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Directory
    {
        public string FullPath { get; }
        public string Name { get; }

        public Directory(string fullPath, string name)
        {
            FullPath = fullPath;
            Name = name;
        }
    }
}
