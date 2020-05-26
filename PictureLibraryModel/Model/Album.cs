using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Album : ILibraryEntity
    {
        public string Name { get; }
        public List<ImageFile> Images { get; }

        public string ImageSource => throw new NotImplementedException();

        public Album(string name)
        {
            Name = name;
            Images = new List<ImageFile>();
        }

        public Album(string name, List<ImageFile> images)
        {
            Name = name;
            Images = images;
        }
    }
}
