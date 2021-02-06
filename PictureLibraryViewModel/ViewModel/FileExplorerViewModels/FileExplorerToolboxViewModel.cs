using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerToolboxViewModel : IFileExplorerToolboxViewModel
    {
        private IDirectoryService _directoryService;
        private IFileService _fileService;

        public IFileExplorerViewModel CommonViewModel { get; }
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

        public FileExplorerToolboxViewModel(IFileExplorerViewModel viewModel, IFileService fileService, IDirectoryService directoryService, IClipboardService clipboard)
        {
            CommonViewModel = viewModel;
            _fileService = fileService;
            _directoryService = directoryService;
            Clipboard = clipboard;
            CommonViewModel.SelectedElements.CollectionChanged += OnSelectedElementsChanged;
            Clipboard.ClipboardContentChanged += OnClipboardContentChanged;
            CommonViewModel.PropertyChanged += OnCurrentDirectoryPathChanged;
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

        private void OnCurrentDirectoryPathChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != "CurrentDirectoryPath") return;

            (BackCommand as BackCommand).OnExecuteChanged();
            (ForwardCommand as ForwardCommand).OnExecuteChanged();
            (GoToParentDirectoryCommand as GoToParentDirectoryCommand).OnExecuteChanged();
        }
        #endregion

        #region Public methods
        public void Copy()
        {
            var paths = new List<string>();

            foreach (var t in CommonViewModel.SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            Clipboard.SetFiles(paths, ClipboardFilesState.Copied);
        }

        public void CopyPath()
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

            Clipboard.SetText(text);
        }

        public void CreateDirectory()
        {
            throw new NotImplementedException();
        }

        public void GoToParentDirectory()
        {
            var parent = (_directoryService.GetInfo(CommonViewModel.CurrentlyOpenedPath) as DirectoryInfo).Parent?.FullName;
            if (parent != null) CommonViewModel.CurrentlyOpenedPath = parent;
        }

        public void Paste()
        {
            var paths = Clipboard.GetFiles();
            string directoryPath;

            if (CommonViewModel.CurrentlyOpenedPath.EndsWith("\\")) directoryPath = CommonViewModel.CurrentlyOpenedPath;
            else directoryPath = CommonViewModel.CurrentlyOpenedPath + "\\";

            foreach (var t in paths)
            {
                if (_directoryService.IsDirectory(t))
                {
                    var directoryName = _directoryService.GetInfo(t).Name;

                    if (Clipboard.FilesState == ClipboardFilesState.Copied)
                    {
                        _directoryService.Copy(t, directoryPath + directoryName);
                    }
                    else if (Clipboard.FilesState == ClipboardFilesState.Cut)
                    {
                        _directoryService.Move(t, directoryPath + directoryName);
                    }
                }
                else
                {
                    var fileName = _fileService.GetInfo(t).Name;

                    if (Clipboard.FilesState == ClipboardFilesState.Copied)
                    {
                        _fileService.Copy(t, directoryPath + fileName);
                    }
                    else if (Clipboard.FilesState == ClipboardFilesState.Cut)
                    {
                        _fileService.Move(t, directoryPath + fileName);
                    }
                }
            }

            Clipboard.Clear();

            CommonViewModel.LoadCurrentDirectoryContent();
        }

        public void Refresh()
        {
            CommonViewModel.LoadCurrentDirectoryContent();
        }

        public void Remove()
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

            CommonViewModel.LoadCurrentDirectoryContent();
        }

        public void Rename()
        {
            throw new NotImplementedException();
        }

        public void Cut()
        {
            var paths = new List<string>();

            foreach (var t in CommonViewModel.SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            Clipboard.SetFiles(paths, ClipboardFilesState.Cut);
        }
        #endregion
    }
}
