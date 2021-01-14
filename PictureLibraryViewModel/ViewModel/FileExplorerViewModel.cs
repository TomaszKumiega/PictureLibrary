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

        public ObservableCollection<IFileSystemEntity> DirectoryTree { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; private set; }
        public string CurrentDirectoryPath { get; set; }
        public ICommand CopyFileCommand { get; }
        public ICommand PasteCommand { get; }
        public IExplorableElement SelectedFile { get; set; }
        public IExplorableElement SelectedNode { get; set; }
        public IClipboardService Clipboard { get; }

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            _fileSystemService = fileSystemService;
            Clipboard = clipboard;
            CopyFileCommand = commandFactory.GetCopyCommand(this);
            PasteCommand = commandFactory.GetPasteCommand(this);
        }

        public void Copy()
        {
            Clipboard.CopiedElement = SelectedFile;
        }

        public void Cut()
        {
            Clipboard.CutElement = SelectedFile;
        }

        public void Paste()
        {
            if(Clipboard.CopiedElement != null)
            {
                _fileSystemService.Copy(Clipboard.CopiedElement as IFileSystemEntity, CurrentDirectoryPath);
                Clipboard.CopiedElement = null;
            }
            else if(Clipboard.CutElement != null)
            {
                _fileSystemService.Move(Clipboard.CutElement as IFileSystemEntity, CurrentDirectoryPath);
                Clipboard.CutElement = null;
            }
        }
    }
}
