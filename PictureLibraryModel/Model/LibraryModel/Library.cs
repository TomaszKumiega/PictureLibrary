using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    [XmlInclude(typeof(LocalLibrary))]
    [XmlInclude(typeof(GoogleDriveLibrary))]
    public abstract class Library : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
