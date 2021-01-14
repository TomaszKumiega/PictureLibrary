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
        private IExplorableElement _cutFile;
        private IExplorableElement _copiedFile;

        public ObservableCollection<IFileSystemEntity> DirectoryTree { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; private set; }
        public string CurrentDirectoryPath { get; set; }
        public ICommand CopyFileCommand { get; }
        public IExplorableElement SelectedFile { get; set; }
        public IExplorableElement SelectedNode { get; set; }

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory)
        {
            _fileSystemService = fileSystemService;
            CopyFileCommand = commandFactory.GetCopyFileCommand(this);
        }

        public IExplorableElement CutFile
        {
            get => _cutFile;

            set
            {
                _cutFile = value;
                _copiedFile = null;
            }
        }

        public IExplorableElement CopiedFile
        {
            get => _copiedFile;

            set
            {
                _copiedFile = value;
                _cutFile = null;
            }
        }

        public void Copy()
        {
            CopiedFile = SelectedFile;
        }

        public void Cut()
        {
            CutFile = SelectedFile;
        }

        public void Paste()
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
