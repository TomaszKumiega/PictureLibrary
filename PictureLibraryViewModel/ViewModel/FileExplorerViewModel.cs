using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        private FileSystemService _fileSystemService;
        private IExplorableElement _selectedNode;
        private string _currentDirectoryPath;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IExplorableElement> DirectoryTree { get; private set; }
        public ObservableCollection<IExplorableElement> CurrentDirectoryFiles { get; private set; }
        public ICommand CopyFileCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand CopyPathCommand { get; }
        public IExplorableElement SelectedFile { get; set; }
        public IClipboardService Clipboard { get; }

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            _fileSystemService = fileSystemService;
            Clipboard = clipboard;

            CopyFileCommand = commandFactory.GetCopyCommand(this);
            PasteCommand = commandFactory.GetPasteCommand(this);
            CutCommand = commandFactory.GetCutCommand(this);
            CopyPathCommand = commandFactory.GetCopyPathCommand(this);

            InitializeDirectoryTree();
            InitializeCurrentDirectoryFiles();
        }

        public IExplorableElement SelectedNode 
        { 
            get => _selectedNode; 
            set
            {
                _selectedNode = value;
                CurrentDirectoryPath = _selectedNode.FullPath;
            }
        }

        public string CurrentDirectoryPath 
        {
            get => _currentDirectoryPath; 
            set
            {
                _currentDirectoryPath = value;
                ReloadCurrentDirectoryFiles();
            }
        }

        private void InitializeDirectoryTree()
        {
            DirectoryTree = new ObservableCollection<IExplorableElement>();

            var rootDirectories = _fileSystemService.GetRootDirectories();
            
            foreach(var t in rootDirectories)
            {
                Task.Run(() => t.LoadSubDirectoriesAsync()).Wait();
                DirectoryTree.Add(t);
            }
        }

        private void InitializeCurrentDirectoryFiles()
        {
            CurrentDirectoryFiles = new ObservableCollection<IExplorableElement>();

            foreach (var t in DirectoryTree)
            {
                CurrentDirectoryFiles.Add(t);
            }
        }

        private void ReloadCurrentDirectoryFiles()
        {
            CurrentDirectoryFiles.Clear();

            foreach (var t in _fileSystemService.GetDirectoryContent(CurrentDirectoryPath))
            {
                CurrentDirectoryFiles.Add(t);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDirectoryFiles"));
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
                _fileSystemService.Copy(Clipboard.CopiedElement, CurrentDirectoryPath);
                Clipboard.CopiedElement = null;
            }
            else if(Clipboard.CutElement != null)
            {
                _fileSystemService.Move(Clipboard.CutElement, CurrentDirectoryPath);
                Clipboard.CutElement = null;
            }
        }

        public void CopyPath()
        {
            Clipboard.SystemClipboard = SelectedFile.FullPath;
        }
    }
}
