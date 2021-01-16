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

        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }
        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; private set; }
        public ICommand CopyFileCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand CopyPathCommand { get; }
        public List<IExplorableElement> SelectedElements { get; set; }
        public IClipboardService Clipboard { get; }

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            _fileSystemService = fileSystemService;
            Clipboard = clipboard;

            SelectedElements = new List<IExplorableElement>();

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
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            var rootDirectories = _fileSystemService.GetRootDirectories();
            
            foreach(var t in rootDirectories)
            {
                Task.Run(() => t.LoadSubDirectoriesAsync()).Wait();
                ExplorableElementsTree.Add(t);
            }
        }

        private void InitializeCurrentDirectoryFiles()
        {
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();

            foreach (var t in ExplorableElementsTree)
            {
                CurrentlyShownElements.Add(t);
            }
        }

        private void ReloadCurrentDirectoryFiles()
        {
            CurrentlyShownElements.Clear();

            foreach (var t in _fileSystemService.GetDirectoryContent(CurrentDirectoryPath))
            {
                CurrentlyShownElements.Add(t);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyShownElements"));
        }

        public void CopySelectedElements()
        {
            Clipboard.CopiedElements = SelectedElements;
        }

        public void CutSelectedElements()
        {
            Clipboard.CutElements = SelectedElements;
        }

        public void PasteSelectedElements()
        {
            if(Clipboard.CopiedElements != null)
            {
                foreach(var t in Clipboard.CopiedElements)
                {
                    _fileSystemService.Copy(t, CurrentDirectoryPath);
                }

                Clipboard.CopiedElements = null;
            }
            else if(Clipboard.CutElements != null)
            {
                foreach(var t in Clipboard.CutElements)
                {
                    _fileSystemService.Move(t, CurrentDirectoryPath);
                }

                Clipboard.CutElements = null;
            }

            ReloadCurrentDirectoryFiles();
        }

        public void CopyPath()
        {
            var text = "";

            if (SelectedElements.Count == 1) text = SelectedElements[0].FullPath;
            else
            {
                foreach (var t in SelectedElements)
                {
                    text += t.FullPath + "\n";
                }
            }
            
            Clipboard.SystemClipboard = text;
        }

        public void RemoveSelectedElements()
        {
            foreach(var t in SelectedElements)
            {
                _fileSystemService.Remove(t);
            }

            ReloadCurrentDirectoryFiles();
        }
    }
}
