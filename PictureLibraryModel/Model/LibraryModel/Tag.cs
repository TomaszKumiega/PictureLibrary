using System;
using System.Drawing;
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
        public Image Icon { get; private set; }

        public Tag()
        {
        }

        public void LoadIcon()
        {
            //TODO: fix
            Icon = Image.FromFile(".\\Icons\\FolderIcon.png");
        }

        ~Tag()
        {
            //TODO: fix
            Icon?.Dispose();
        }
    }
}
