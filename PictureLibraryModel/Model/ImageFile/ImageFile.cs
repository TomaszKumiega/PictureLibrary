using System;
using System.Collections.Generic;

namespace PictureLibraryModel.Model
{
    public abstract class ImageFile : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Extension { get; set; }
        public List<Tag> Tags { get; set; }

        protected ImageFile()
        {
            Id = Guid.NewGuid();
            Tags = new List<Tag>();
        }
    }
}
