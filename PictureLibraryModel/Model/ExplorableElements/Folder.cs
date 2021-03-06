﻿using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Folder : Directory
    {
        public Folder() : base()
        {

        }

        public Folder(IDirectoryService directoryService) : base(directoryService)
        {

        }

        public Folder(IDirectoryService directoryService, DirectoryInfo directoryInfo) : base(directoryService)
        {
            Name = directoryInfo.Name;
            FullName = directoryInfo.FullName;
            Origin = Origin.Local;
        }

        public Folder(string path, string name, IDirectoryService directoryService, Origin origin) : base(path, name, directoryService, origin)
        {

        }
    }
}
