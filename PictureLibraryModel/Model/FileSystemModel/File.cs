using ImageMagick;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    public abstract class File : IFileSystemElement
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        [XmlIgnore]
        public MagickImage Icon { get; protected set; }
        public string Path { get; set; }
        
        public File()
        {
          
        }

        public abstract void LoadIcon();

        public virtual bool Exists()
        {
            return System.IO.File.Exists(Path);
        }
    }
}
