using System;

namespace PictureLibraryModel.Model
{
    public abstract class Library : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
