using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        public string CurrentDirectory { get; }

        public ObservableCollection<Drive> Drives { get; private set; }

        private IFileSystemService FileSystemService { get; }

        public FileSystemViewModel(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            Initialize();
        }

        private void Initialize()
        {
            Drives = FileSystemService.GetDrives();
        }
    }
}
