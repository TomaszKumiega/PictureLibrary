using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        private IDirectoryService _directoryService;
        private IFileService _fileService;
        private IExplorableElement _selectedNode;
        private string _currentDirectoryPath;

        public event PropertyChangedEventHandler PropertyChanged;
        
        #region Public Properties
        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }
        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; private set; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand CopyPathCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand RenameCommand { get; }
        public ICommand CreateFolderCommand { get; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
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

        public FileExplorerViewModel(IDirectoryService directoryService, IFileService fileService, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            _directoryService = directoryService;
            _fileService = fileService;
            Clipboard = clipboard;

            SelectedElements = new ObservableCollection<IExplorableElement>();
            SelectedElements.CollectionChanged += OnSelectedElementsChanged;
            Clipboard.ClipboardContentChanged += OnCopiedElementsChanged;
            Clipboard.ClipboardContentChanged += OnCutElementsChanged;

            #region Command Initialization
            CopyCommand = commandFactory.GetCopyCommand(this);
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

        #region Events Handlers
        private void OnSelectedElementsChanged(object o, EventArgs args)
        {
            (CopyCommand as CopyCommand).OnExecuteChanged();
            (CutCommand as CutCommand).OnExecuteChanged();
            (CopyPathCommand as CopyPathCommand).OnExecuteChanged();
            (RemoveCommand as RemoveCommand).OnExecuteChanged();
        }

        private void OnCopiedElementsChanged(object o, EventArgs args)
        {
            (PasteCommand as PasteCommand).OnExecuteChanged();
        }

        private void OnCutElementsChanged(object o, EventArgs args)
        {
            (PasteCommand as PasteCommand).OnExecuteChanged();
        }
        #endregion

        #region Initialize methods
        private void InitializeDirectoryTree()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            var rootDirectories = _directoryService.GetRootDirectories();
            
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

            foreach (var t in _directoryService.GetDirectoryContent(CurrentDirectoryPath))
            {
                CurrentlyShownElements.Add(t);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyShownElements"));
        }

        public void CopySelectedElements()
        {
            var paths = new List<string>();

            foreach(var t in SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            Clipboard.SetFiles(paths, ClipboardFilesState.Copied);
        }

        public void CutSelectedElements()
        {
            var paths = new List<string>();

            foreach(var t in SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            Clipboard.SetFiles(paths, ClipboardFilesState.Cut);
        }

        public void PasteSelectedElements()
        {
            var paths = Clipboard.GetFiles();

            foreach(var t in paths)
            {
                if(_directoryService.IsDirectory(t))
                {
                    var directoryName = _directoryService.GetInfo(t).Name;

                    if (Clipboard.FilesState == ClipboardFilesState.Copied)
                    { 
                        if (CurrentDirectoryPath.EndsWith("\\"))
                        {
                            _directoryService.Copy(t, CurrentDirectoryPath + directoryName);
                            Clipboard.Clear();
                        }
                        else
                        {
                            _directoryService.Copy(t, CurrentDirectoryPath + '\\' + directoryName);
                            Clipboard.Clear();
                        }
                    }
                    else if(Clipboard.FilesState == ClipboardFilesState.Cut)
                    {
                        if (CurrentDirectoryPath.EndsWith("\\"))
                        {
                            _directoryService.Move(t, CurrentDirectoryPath + directoryName);
                            Clipboard.Clear();
                        }
                        else
                        {
                            _directoryService.Move(t, CurrentDirectoryPath + "\\" + directoryName);
                            Clipboard.Clear();
                        }
                    }
                }
                else
                {
                    var fileName = _fileService.GetInfo(t).Name;

                    if (Clipboard.FilesState == ClipboardFilesState.Copied)
                    {
                        if (CurrentDirectoryPath.EndsWith("\\"))
                        {
                            _fileService.Copy(t, CurrentDirectoryPath + fileName);
                            Clipboard.Clear();
                        }
                        else
                        {
                            _fileService.Copy(t, CurrentDirectoryPath + '\\' + fileName);
                            Clipboard.Clear();
                        }
                    }
                    else if (Clipboard.FilesState == ClipboardFilesState.Cut)
                    {
                        if (CurrentDirectoryPath.EndsWith("\\"))
                        {
                            _fileService.Move(t, CurrentDirectoryPath + fileName);
                            Clipboard.Clear();
                        }
                        else
                        {
                            _fileService.Move(t, CurrentDirectoryPath + '\\' + fileName);
                            Clipboard.Clear();
                        }
                    }
                }
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

            Clipboard.SetText(text);
        }

        public void RemoveSelectedElements()
        {
            foreach(var t in SelectedElements)
            {
                if(t is File)
                {
                    _fileService.Remove(t.FullPath);
                }
                else if(t is Directory)
                {
                    _directoryService.Remove(t.FullPath);
                }
            }

            ReloadCurrentDirectoryFiles();
        }

        public void RenameSelectedElements()
        {
            if (SelectedElements == null) throw new ArgumentNullException();
            
            for(int i=0; i<SelectedElements.Count; i++)
            {
                if (SelectedElements[i] is File)
                {
                    _fileService.Rename(SelectedElements[i].FullPath, SelectedElements[i].Name + (i > 0 ? i.ToString() : ""));
                }
                else if (SelectedElements[i] is Directory)
                {
                    _directoryService.Rename(SelectedElements[i].FullPath, SelectedElements[i].Name + (i > 0 ? i.ToString() : ""));
                }
            }
        }

        public void CreateDirectory(string path)
        { 
            _directoryService.Create(path);    
        }
        #endregion
    }
}
