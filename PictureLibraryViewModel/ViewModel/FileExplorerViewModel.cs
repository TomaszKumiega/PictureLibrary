using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        private FileSystemService _fileSystemService;
        private IExplorableElement CopiedFile { get; set; }
        public ObservableCollection<IFileSystemEntity> DirectoryTree { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; private set; }
        public string CurrentDirectoryPath { get; set; }
        public CopyFileCommand CopyFileCommand { get; set; }
        public IExplorableElement SelectedFile { get; set; }
        public IExplorableElement SelectedNode { get; set; }

        public FileExplorerViewModel(FileSystemService fileSystemService, CopyFileCommand copyFileCommand)
        {
            _fileSystemService = fileSystemService;
            CopyFileCommand = copyFileCommand;
        }

        public void CopyFile()
        {
            CopiedFile = SelectedFile;
        }
    }
}
