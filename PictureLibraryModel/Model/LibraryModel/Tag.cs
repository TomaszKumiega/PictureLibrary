using System;

namespace PictureLibraryModel.Model
{
    public class Tag : IEntity
    { 
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }

        public Tag()
        {
            Id = Guid.NewGuid();
        }
    }
}
