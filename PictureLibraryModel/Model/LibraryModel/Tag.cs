using ImageMagick;
using System;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    public class Tag : IExplorableElement
    { 
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? RemoteStorageInfoId { get; set; }
        public string Path { get; set; }
        public string Color { get; set; }
        [XmlIgnore]
        public MagickImage Icon { get; private set; }

        public Tag()
        {
        }

        public void LoadIcon()
        {
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;

            Icon = new MagickImage(".\\Icons\\FolderIcon.png", settings);
        }

        ~Tag()
        {
            Icon?.Dispose();
        }
    }
}
