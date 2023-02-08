using ImageMagick;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    public abstract class File
    {
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
