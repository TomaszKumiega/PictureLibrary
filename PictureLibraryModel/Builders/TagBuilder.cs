using PictureLibraryModel.Model;
using System;

namespace PictureLibraryModel.Builders
{
    public class TagBuilder : ITagBuilder
    {
        private Tag? _tag;

        public ITagBuilder CreateTag()
        {
            _tag = new Tag()
            {
                Id = Guid.NewGuid(),
            };

            return this;
        }

        public Tag GetTag()
        {
            return _tag ?? throw new InvalidOperationException("Create object first.");
        }

        public ITagBuilder WithDescription(string? description)
        {
            _tag!.Description = description;

            return this;
        }

        public ITagBuilder WithColor(string color)
        {
            _tag!.Color = color;

            return this;
        }

        public ITagBuilder WithName(string name)
        {
            _tag!.Name = name;

            return this;
        }
    }
}
