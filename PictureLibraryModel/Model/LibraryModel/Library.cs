using ImageMagick;
using PictureLibraryModel.Model.LibraryModel;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    [XmlInclude(typeof(LocalLibrary))]
    [XmlInclude(typeof(GoogleDriveLibrary))]
    public abstract class Library : IExplorableElement
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        [XmlIgnore]
        public MagickImage Icon { get; protected set; }

        abstract public void LoadIcon();

        ~Library()
        {
            Icon?.Dispose();
        }
    }
}
