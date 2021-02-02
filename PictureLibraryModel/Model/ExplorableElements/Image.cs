using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Image
    {
        public ImageFile ImageFile { get; set; }
        public byte[] ImageContent { get; set; }
    }
}
