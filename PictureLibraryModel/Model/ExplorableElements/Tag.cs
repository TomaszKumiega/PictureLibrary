using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Tag : IExplorableElement
    { 
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Origin { get; set; }
        public string FullName { get; set; }
        public string IconSource { get; }
        public string Color { get; set; }

        public Tag()
        {
            IconSource = "pack://application:,,,/Icons/FolderIcon.png";
        }
    }
}
