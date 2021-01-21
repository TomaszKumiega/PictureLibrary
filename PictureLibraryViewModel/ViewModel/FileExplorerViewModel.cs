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

        #region Public Properties
        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }
        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; private set; }
        public ICommand CopyFileCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand CopyPathCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand RenameCommand { get; }
        public ICommand CreateFolderCommand { get; }
        public List<IExplorableElement> SelectedElements { get; set; }
        public IClipboardService Clipboard { get; }

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDirectoryPath"));
                ReloadCurrentDirectoryFiles();
            }
        }
        #endregion

        public FileExplorerViewModel(FileSystemService fileSystemService, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            _fileSystemService = fileSystemService;
            Clipboard = clipboard;

            SelectedElements = new List<IExplorableElement>();

            #region Command Initialization
            CopyFileCommand = commandFactory.GetCopyCommand(this);
            PasteCommand = commandFactory.GetPasteCommand(this);
            CutCommand = commandFactory.GetCutCommand(this);
            CopyPathCommand = commandFactory.GetCopyPathCommand(this);
            RemoveCommand = commandFactory.GetRemoveCommand(this);
            RenameCommand = commandFactory.GetRenameCommand(this);
            CreateFolderCommand = commandFactory.GetCreateFolderCommand(this);
            #endregion

            InitializeDirectoryTree();
            InitializeCurrentDirectoryFiles();
        }

        #region Initialize methods
        private void InitializeDirectoryTree()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            var rootDirectories = _fileSystemService.GetRootDirectories();
            
            foreach(var t in rootDirectories)
            {
                Task.Run(() => t.LoadSubDirectoriesAsync()).Wait();
                ExplorableElementsTree.Add(t);
            }

            CurrentDirectoryPath = "\\";
        }

        private void InitializeCurrentDirectoryFiles()
        {
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();

            foreach (var t in ExplorableElementsTree)
            {
                CurrentlyShownElements.Add(t);
            }
        }
        #endregion

        #region ViewModel Logic
        private void ReloadCurrentDirectoryFiles()
        {
            if (CurrentlyShownElements == null) return;
            
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

        public void RenameSelectedElements()
        {
            if (SelectedElements == null) throw new ArgumentNullException();
            
            for(int i=0; i<SelectedElements.Count; i++)
            {
                _fileSystemService.Rename(SelectedElements[i], SelectedElements[i].Name + (i > 0 ? i.ToString() : ""));
            }
        }

        public void CreateDirectory(string path)
        { 
            _fileSystemService.CreateDirectory(path);    
        }
        #endregion
    }
}
