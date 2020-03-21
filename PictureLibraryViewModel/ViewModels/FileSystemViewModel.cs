using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        public string CurrentDirectory => throw new NotImplementedException();

        public ObservableCollection<Drive> Drives => throw new NotImplementedException();
    }
}
