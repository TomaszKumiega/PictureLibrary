using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        private FileSystemService _fileSystemService;
        public ObservableCollection<IFileSystemEntity> DirectoryTree { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; private set; }
        public string CurrentDirectoryPath { get; set; }

        public FileExplorerViewModel(FileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }
    }
}
