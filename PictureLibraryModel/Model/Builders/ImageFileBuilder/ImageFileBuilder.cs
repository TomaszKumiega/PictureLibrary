using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureLibraryModel.Model.Builders.ImageFileBuilder
{
    public class ImageFileBuilder : IImageFileBuilder
    {
        private ImageFile ImageFile { get; set; }

        public IImageFileBuilder StartBuilding()
        {
            ImageFile = new ImageFile();
            return this;
        }

        public IImageFileBuilder From(Origin origin)
        {
            ImageFile.Origin = origin;
            return this;
        }

        public IImageFileBuilder WithCreationTime(DateTime creationTime)
        {
            ImageFile.CreationTime = creationTime;
            return this;
        }

        public IImageFileBuilder WithExtension(ImageExtension extension)
        {
            ImageFile.Extension = extension;
            return this;
        }

        public IImageFileBuilder WithExtension(string extension)
        {
            ImageFile.Extension = ImageExtensionHelper.GetExtension(extension);
            return this;
        }

        public IImageFileBuilder WithFullName(string fullName)
        {
            ImageFile.FullName = fullName;
            return this;
        }

        public IImageFileBuilder WithLastAccessTime(DateTime lastAccessTime)
        {
            ImageFile.LastAccessTime = lastAccessTime;
            return this;
        }

        public IImageFileBuilder WithLastWriteTime(DateTime lastWriteTime)
        {
            ImageFile.LastWriteTime = lastWriteTime;
            return this;
        }

        public IImageFileBuilder WithLibraryFullName(string libraryFullName)
        {
            ImageFile.LibraryFullName = libraryFullName;
            return this;
        }

        public IImageFileBuilder WithName(string name)
        {
            ImageFile.Name = name;
            return this;
        }

        public IImageFileBuilder WithSize(long bytes)
        {
            ImageFile.Size = bytes;
            return this;
        }

        public IImageFileBuilder WithTags(IEnumerable<Tag> tags)
        {
            ImageFile.Tags = tags.ToList();
            return this;
        }

        public ImageFile Build()
        {
            return ImageFile;
        }
    }
}
