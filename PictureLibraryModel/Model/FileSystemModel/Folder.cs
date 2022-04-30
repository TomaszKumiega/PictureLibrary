﻿using PictureLibraryModel.Services.FileSystemServices;

namespace PictureLibraryModel.Model
{
    public class Folder : Directory
    {
        public Folder() : base()
        {

        }

        public Folder(string path, string name, IDirectoryService directoryService) : base(path, name, directoryService)
        {

        }

        ~Folder()
        {
            Icon?.Dispose();
        }
    }
}
