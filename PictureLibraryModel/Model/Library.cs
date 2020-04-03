using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Library
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }
}
