using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Album
    {
        public string Name { get; }
        public List<ImageFile> Images { get; }

        public Album(string name)
        {
            Name = name;
            Images = new List<ImageFile>();
        }
    }
}
