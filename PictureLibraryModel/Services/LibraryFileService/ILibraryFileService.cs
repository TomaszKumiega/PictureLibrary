﻿using PictureLibraryModel.Model;
using System;
using System.IO;

namespace PictureLibraryModel.Services.LibraryFileService
{
    public interface ILibraryFileService
    {
        void WriteLibraryToStreamAsync(Stream fileStream, Library library);
        Library ReadLibraryFromStreamAsync(Stream fileStream);
        Library ReloadLibrary(Library library);
    }
}
