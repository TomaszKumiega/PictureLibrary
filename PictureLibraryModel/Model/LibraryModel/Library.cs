using ImageMagick;
using PictureLibraryModel.Model.LibraryModel;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    [XmlInclude(typeof(LocalLibrary))]
    public class Library : IExplorableElement
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        [XmlIgnore]
        public MagickImage Icon { get; private set; }

        public Library()
        {
            
        }

        public void LoadIcon()
        {
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;

            Icon = new MagickImage(".\\Icons\\LibraryIcon.png", settings);
        }

        ~Library()
        {
            Icon?.Dispose();
        }
    }
}
