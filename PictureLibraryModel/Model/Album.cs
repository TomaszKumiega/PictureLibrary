using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Album
    {
        public string Name { get; set; }
        public List<ImageFile> Images { get; set; }
    }
}
