using System;
using System.Collections.Generic;
using System.Drawing;

namespace PictureLibraryModel.Model
{
    public class Library : IExplorableElement
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        public Guid RemoteStorageInfoId { get; set; }
        public Image Icon { get; private set; }

        public Library()
        {
            
        }

        public void LoadIcon()
        {
            Icon = Image.FromFile(".\\Icons\\LibraryIcon.png");
        }

        ~Library()
        {
            Icon?.Dispose();
        }
    }
}
