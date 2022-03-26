﻿using System;

namespace PictureLibraryModel.Model.Builders
{
    public interface ILibraryBuilder
    {
        ILibraryBuilder CreateLibrary();
        ILibraryBuilder WithName(string name);
        ILibraryBuilder WithDescription(string description);
        ILibraryBuilder WithStorageInfoId(Guid? storageInfoId);
        ILibraryBuilder WithPath(string path);
        Library Build();
    }
}
