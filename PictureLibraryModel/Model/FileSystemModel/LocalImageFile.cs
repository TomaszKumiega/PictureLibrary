using ImageMagick;
using System;

namespace PictureLibraryModel.Model
{
    public class LocalImageFile : ImageFile
    {
        public LocalImageFile() : base()
        {

        }

        public string Path { get; set; }
    }
}
