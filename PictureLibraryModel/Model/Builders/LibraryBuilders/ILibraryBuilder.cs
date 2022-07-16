using System;

namespace PictureLibraryModel.Model.Builders
{
    public interface ILibraryBuilder
    {
        ILibraryBuilder CreateLibrary();
        ILibraryBuilder WithName(string name);
        ILibraryBuilder WithDescription(string description);
        ILibraryBuilder WithPath(string path);
        Library Build();
    }
}
