using ImageMagick;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    public abstract class File : IExplorableElement
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        [XmlIgnore]
        public MagickImage Icon { get; protected set; }
        public string Path { get; set; }
        
        protected File()
        {
          
        }

        public abstract void LoadIcon();
    }
}
