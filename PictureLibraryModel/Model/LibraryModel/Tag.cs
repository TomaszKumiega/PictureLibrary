using System;
using System.Drawing;

namespace PictureLibraryModel.Model
{
    public class Tag : IExplorableElement
    { 
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid RemoteStorageInfoId { get; set; }
        public string Path { get; set; }
        public string Color { get; set; }
        public Image Icon { get; private set; }

        public Tag()
        {
        }

        public void LoadIcon()
        {
            Icon = Image.FromFile(".\\Icons\\FolderIcon.png");
        }

        public override bool Equals(object obj)
        {
            if (obj is Tag tag)
            {
                return tag.Name == Name && tag.Description == Description && tag.Path == Path && tag.Color == Color && tag.RemoteStorageInfoId == RemoteStorageInfoId;
            }

            return false;
        }

        ~Tag()
        {
            Icon?.Dispose();
        }
    }
}
