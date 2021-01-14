using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
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
        private IClipboardService _clipboard;

        public ObservableCollection<IFileSystemEntity> DirectoryTree { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; private set; }
        public string CurrentDirectoryPath { get; set; }
        public ICommand CopyFileCommand { get; }
        public ICommand PasteCommand { get; }
        public IExplorableElement SelectedFile { get; set; }
        public IExplorableElement SelectedNode { get; set; }

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            _fileSystemService = fileSystemService;
            _clipboard = clipboard;
            CopyFileCommand = commandFactory.GetCopyCommand(this);
            PasteCommand = commandFactory.GetPasteCommand(this);
        }

        public void Copy()
        {
            _clipboard.CopiedElement = SelectedFile;
        }

        public void Cut()
        {
            _clipboard.CutElement = SelectedFile;
        }

        public void Paste()
        {
            if(_clipboard.CopiedElement != null)
            {
                _fileSystemService.Copy(_clipboard.CopiedElement as IFileSystemEntity, CurrentDirectoryPath);
                _clipboard.CopiedElement = null;
            }
            else if(_clipboard.CutElement != null)
            {
                _fileSystemService.Move(_clipboard.CutElement as IFileSystemEntity, CurrentDirectoryPath);
                _clipboard.CutElement = null;
            }
        }
    }
}
