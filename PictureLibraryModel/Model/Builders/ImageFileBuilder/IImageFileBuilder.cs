using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.Builders.ImageFileBuilder
{
    public interface IImageFileBuilder
    {
        IImageFileBuilder StartBuilding();
        IImageFileBuilder WithName(string name);
        IImageFileBuilder WithExtension(ImageExtension extension);
        IImageFileBuilder WithExtension(string extension);
        IImageFileBuilder WithFullName(string fullName);
        IImageFileBuilder WithLibraryFullName(string libraryFullName);
        IImageFileBuilder WithCreationTime(DateTime creationTime);
        IImageFileBuilder WithLastAccessTime(DateTime lastAccessTime);
        IImageFileBuilder WithLastWriteTime(DateTime lastWriteTime);
        IImageFileBuilder WithSize(long bytes);
        IImageFileBuilder WithTags(IEnumerable<Tag> tags);
        IImageFileBuilder From(Origin origin);
        ImageFile Build();
    }
}
