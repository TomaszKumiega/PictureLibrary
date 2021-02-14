using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerToolboxViewModel : IFileExplorerToolboxViewModel
    {
        private IDirectoryService _directoryService;
        private IFileService _fileService;

        public IExplorerViewModel CommonViewModel { get; }
        public IClipboardService Clipboard { get; }
        public string SearchText { get; set; }

        #region Commands
        public ICommand CopyCommand { get; }

        public ICommand PasteCommand { get; }

        public ICommand CutCommand { get; }

        public ICommand CopyPathCommand { get; }

        public ICommand RemoveCommand { get; }

        public ICommand RenameCommand { get; }

        public ICommand CreateFolderCommand { get; }

        public ICommand BackCommand { get; }

        public ICommand ForwardCommand { get; }

        public ICommand GoToParentDirectoryCommand { get; }

        public ICommand RefreshCommand { get; }
        #endregion

        public FileExplorerToolboxViewModel(IFileExplorerViewModel viewModel, IFileService fileService, IDirectoryService directoryService, IClipboardService clipboard, ICommandFactory commandFactory)
        {
            CommonViewModel = viewModel;
            _fileService = fileService;
            _directoryService = directoryService;
            Clipboard = clipboard;

            #region Command Initialization
            CopyCommand = commandFactory.GetCopyCommand(this);
            PasteCommand = commandFactory.GetPasteCommand(this);
            CutCommand = commandFactory.GetCutCommand(this);
            CopyPathCommand = commandFactory.GetCopyPathCommand(this);
            RemoveCommand = commandFactory.GetRemoveCommand(this);
            RenameCommand = commandFactory.GetRenameCommand(this);
            CreateFolderCommand = commandFactory.GetCreateFolderCommand(this);
            BackCommand = commandFactory.GetBackCommand(this);
            ForwardCommand = commandFactory.GetForwardCommand(this);
            GoToParentDirectoryCommand = commandFactory.GetGoToParentDirectoryCommand(this);
            RefreshCommand = commandFactory.GetRefreshCommand(this);
            #endregion

            CommonViewModel.SelectedElements.CollectionChanged += OnSelectedElementsChanged;
            Clipboard.ClipboardContentChanged += OnClipboardContentChanged;
            CommonViewModel.PropertyChanged += OnCurrentlyOpenedElementChanged;
        }


        #region Event Handler methods
        private void OnSelectedElementsChanged(object o, EventArgs args)
        {
            (CopyCommand as CopyCommand).OnExecuteChanged();
            (CutCommand as CutCommand).OnExecuteChanged();
            (CopyPathCommand as CopyPathCommand).OnExecuteChanged();
            (RemoveCommand as RemoveCommand).OnExecuteChanged();
        }

        private void OnClipboardContentChanged(object o, EventArgs args)
        {
            (PasteCommand as PasteCommand).OnExecuteChanged();
        }

        private void OnCurrentlyOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != "CurrentlyOpenedElement") return;

            (BackCommand as BackCommand).OnExecuteChanged();
            (ForwardCommand as ForwardCommand).OnExecuteChanged();
            (GoToParentDirectoryCommand as GoToParentDirectoryCommand).OnExecuteChanged();
        }
        #endregion

        #region Public methods
        public async Task Copy()
        {
            var paths = new List<string>();

            foreach (var t in CommonViewModel.SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            await Task.Run(() => Clipboard.SetFiles(paths, ClipboardFilesState.Copied));
        }

        public async Task CopyPath()
        {
            var text = "";

            if (CommonViewModel.SelectedElements.Count == 1) text = CommonViewModel.SelectedElements[0].FullPath;
            else
            {
                foreach (var t in CommonViewModel.SelectedElements)
                {
                    text += t.FullPath + "\n";
                }
            }

            await Task.Run(() => Clipboard.SetText(text));
        }

        public async Task CreateDirectory()
        {
            throw new NotImplementedException();
        }

        public void GoToParentDirectory()
        {
            var parent = _directoryService.GetParent(CommonViewModel.CurrentlyOpenedElement.FullPath);
            if (parent != null) CommonViewModel.CurrentlyOpenedElement = parent;
        }

        public async Task Paste()
        {
            var paths = Clipboard.GetFiles();
            string directoryPath;

            if (CommonViewModel.CurrentlyOpenedElement.FullPath.EndsWith("\\")) directoryPath = CommonViewModel.CurrentlyOpenedElement.FullPath;
            else directoryPath = CommonViewModel.CurrentlyOpenedElement.FullPath + "\\";

            foreach (var t in paths)
            {
                if (_directoryService.IsDirectory(t))
                {
                    var directoryName = _directoryService.GetInfo(t).Name;

                    if (Clipboard.FilesState == ClipboardFilesState.Copied)
                    {
                        await Task.Run(() => _directoryService.Copy(t, directoryPath + directoryName));
                    }
                    else if (Clipboard.FilesState == ClipboardFilesState.Cut)
                    {
                        await Task.Run(() => _directoryService.Move(t, directoryPath + directoryName));
                    }
                }
                else
                {
                    var fileName = _fileService.GetInfo(t).Name;

                    if (Clipboard.FilesState == ClipboardFilesState.Copied)
                    {
                        await Task.Run(() => _fileService.Copy(t, directoryPath + fileName));
                    }
                    else if (Clipboard.FilesState == ClipboardFilesState.Cut)
                    {
                        await Task.Run(() => _fileService.Move(t, directoryPath + fileName));
                    }
                }
            }

            await Task.Run(() => Clipboard.Clear());
            
            await CommonViewModel.LoadCurrentlyShownElements();
        }

        public async Task Refresh()
        {
            await CommonViewModel.LoadCurrentlyShownElements();
        }

        public async Task Remove()
        {
            foreach (var t in CommonViewModel.SelectedElements)
            {
                if (t is File)
                {
                    _fileService.Remove(t.FullPath);
                }
                else if (t is Directory)
                {
                    _directoryService.Remove(t.FullPath);
                }
            }

            await CommonViewModel.LoadCurrentlyShownElements();
        }

        public async Task Rename()
        {
            throw new NotImplementedException();
        }

        public async Task Cut()
        {
            var paths = new List<string>();

            foreach (var t in CommonViewModel.SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            await Task.Run(() => Clipboard.SetFiles(paths, ClipboardFilesState.Cut));
        }
        #endregion
    }
}
