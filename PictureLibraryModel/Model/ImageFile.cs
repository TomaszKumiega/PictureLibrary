using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class ImageFile
    {
        public FileInfo FileInfo { get; set; }
        public string Path { get; set; }
        public Icon Icon { get; set; }

        public ImageFile()
        {

        }

        public ImageFile(string path)
        {
            if (File.Exists(path))
            {
                Path = path;
                FileInfo = new FileInfo(path);
                Icon = Icon.ExtractAssociatedIcon(path);
            }
            else throw new Exception("File not found");
        }

        public Image GetImage()
        {
            return Image.FromFile(Path);
        }
    }
}
