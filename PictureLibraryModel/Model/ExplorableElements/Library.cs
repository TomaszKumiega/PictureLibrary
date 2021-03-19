using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Library : IExplorableElement
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        public Origin Origin { get; set; }
        public string IconSource { get; }

        public Library()
        {
            IconSource = "pack://application:,,,/Icons/LibraryIcon.png";
        }
    }
}
