using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerToolboxViewModel : IFileExplorerToolboxViewModel
    {
        private IDirectoryService _directoryService;
        private IFileService _fileService;

        public IFileExplorerViewModel FileExplorerViewModel { get; }
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

        public void Copy()
        {
            var paths = new List<string>();

            foreach (var t in FileExplorerViewModel.SelectedElements)
            {
                paths.Add(t.FullPath);
            }

            Clipboard.SetFiles(paths, ClipboardFilesState.Copied);
        }

        public void CopyPath()
        {
            var text = "";

            if (FileExplorerViewModel.SelectedElements.Count == 1) text = FileExplorerViewModel.SelectedElements[0].FullPath;
            else
            {
                foreach (var t in FileExplorerViewModel.SelectedElements)
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
            var parent = (_directoryService.GetInfo(FileExplorerViewModel.CurrentDirectoryPath) as DirectoryInfo).Parent?.FullName;
            if (parent != null) FileExplorerViewModel.CurrentDirectoryPath = parent;
        }

        public void Paste()
        {
            var paths = Clipboard.GetFiles();
            string directoryPath;

            if (FileExplorerViewModel.CurrentDirectoryPath.EndsWith("\\")) directoryPath = FileExplorerViewModel.CurrentDirectoryPath;
            else directoryPath = FileExplorerViewModel.CurrentDirectoryPath + "\\";

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

            FileExplorerViewModel.LoadCurrentDirectoryContent();
        }

        public void Refresh()
        {
            FileExplorerViewModel.LoadCurrentDirectoryContent();
        }

        public void Remove()
        {
            foreach (var t in FileExplorerViewModel.SelectedElements)
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

            FileExplorerViewModel.LoadCurrentDirectoryContent();
        }

        public void Rename()
        {
            throw new NotImplementedException();
        }
    }
}
