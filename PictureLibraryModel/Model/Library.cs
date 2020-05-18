using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Library
    {
        public string FullPath { get; }
        public string Name { get; }
        public List<Album> Albums { get; }

        public Library(string fullPath, string name)
        {
            FullPath = fullPath;
            Name = name;
            Albums = new List<Album>();
        }
    }
}
