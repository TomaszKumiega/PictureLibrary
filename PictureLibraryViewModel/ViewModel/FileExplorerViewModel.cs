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

        public ObservableCollection<IFileSystemEntity> DirectoryTree { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; private set; }
        public string CurrentDirectoryPath { get; set; }
        public ICommand CopyFileCommand { get; set; }
        public IExplorableElement SelectedFile { get; set; }
        public IExplorableElement SelectedNode { get; set; }

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory)
        {
            _fileSystemService = fileSystemService;
            CopyFileCommand = commandFactory.GetCopyFileCommand(this);
        }

        public IExplorableElement CutFile
        {
            get => CutFile;

            private set
            {
                CopiedFile = null;
                CutFile = value;
            }
        }

        public IExplorableElement CopiedFile
        {
            get => CopiedFile;
            private set
            {
                CutFile = null;
                CopiedFile = value;
            }
        }

        public void CopyFile()
        {
            CopiedFile = SelectedFile;
        }

        public void PasteFile()
        {
            if(CopiedFile != null)
            {
                _fileSystemService.Copy(CopiedFile as IFileSystemEntity, CurrentDirectoryPath);
                CopiedFile = null;
            }
            else if(CutFile != null)
            {
                _fileSystemService.Move(CutFile as IFileSystemEntity, CurrentDirectoryPath);
                CutFile = null;
            }
        }
    }
}
