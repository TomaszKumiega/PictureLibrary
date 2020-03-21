﻿using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        public string CurrentDirectory { get; }

        public ObservableCollection<Drive> Drives { get; }
    }
}
