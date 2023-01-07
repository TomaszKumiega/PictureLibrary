using ImageMagick;
using PictureLibraryModel.Model.LibraryModel;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    [XmlInclude(typeof(LocalLibrary))]
    [XmlInclude(typeof(GoogleDriveLibrary))]
    public abstract class Library : IExplorableElement, IEntity
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        [XmlIgnore]
        public MagickImage Icon { get; protected set; }

        abstract public void LoadIcon();

        protected Library()
        {
            Id = Guid.NewGuid();
        }

        ~Library()
        {
            Icon?.Dispose();
        }
    }
}
