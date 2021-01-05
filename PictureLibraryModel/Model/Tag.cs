using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Tag
    { 
        public string Name { get; set; }
        public string Description { get; set; }

        public Tag()
        {

        }

        public Tag(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
