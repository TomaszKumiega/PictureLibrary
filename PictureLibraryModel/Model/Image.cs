using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class Image
    {
        public string Path { get; set; }
        public Bitmap BitMap { get; set; }
        public FileInfo FileInfo { get; set; }
        
        public Image()
        {

        }

    }
}
