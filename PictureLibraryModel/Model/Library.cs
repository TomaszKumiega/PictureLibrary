using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Library : IExplorableElement
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageFile> Images { get; set; }
        public List<Guid> Owners { get; set; }
        public Origin Origin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Library()
        {
            Tags = new List<Tag>();
            Images = new List<ImageFile>();
        }

        public Library(string fullPath, string name, string decription, List<Guid> owners)
        {
            FullPath = fullPath;
            Name = name;
            Description = decription;
            Owners = owners;
            Tags = new List<Tag>();
            Images = new List<ImageFile>();
        }

        public Library(string fullPath, string name, string description, List<Guid> owners, List<Tag> tags, List<ImageFile> images)
        {
            FullPath = fullPath;
            Name = name;
            Description = description;
            Owners = owners;
            Tags = tags;
            Images = images;
        }
    }
}
