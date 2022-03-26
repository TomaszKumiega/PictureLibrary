using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerToolboxViewModel : IFileExplorerToolboxViewModel
    {
        #region Private fields
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        #endregion

        #region Public properties
        public IExplorerViewModel CommonViewModel { get; }
        public IClipboardService Clipboard { get; }
        public string SearchText { get; set; }
        #endregion

        #region Commands
        [Command]
        public ICommand CopyCommand { get; set; }
        [Command]
        public ICommand PasteCommand { get; set; }
        [Command]
        public ICommand CutCommand { get; set; }
        [Command]
        public ICommand CopyPathCommand { get; set; }
        [Command]
        public ICommand RemoveCommand { get; set; }
        [Command]
        public ICommand RenameCommand { get; set; }
        [Command]
        public ICommand CreateFolderCommand { get; set; }
        [Command]
        public ICommand BackCommand { get; set; }
        [Command]
        public ICommand ForwardCommand { get; set; }
        [Command]
        public ICommand GoToParentDirectoryCommand { get; set; }
        [Command]
        public ICommand RefreshCommand { get; set; }
        #endregion

        public FileExplorerToolboxViewModel(
            IFileExplorerViewModel viewModel, 
            IFileService fileService, 
            IDirectoryService directoryService, 
            IClipboardService clipboard, 
            ICommandCreator commandCreator)
        {
            CommonViewModel = viewModel;
            _fileService = fileService;
            _directoryService = directoryService;
            Clipboard = clipboard;

            commandCreator.InitializeCommands(this);
        }

        #region Private methods
        private bool IsDriveSelected()
        {
            bool isDriveSelected = false;

            foreach (var t in CommonViewModel.SelectedElements)
            {
                if (t is PictureLibraryModel.Model.Drive)
                {
                    isDriveSelected = true;
                    break;
                }
            }

            return isDriveSelected;
        }
        #endregion

        #region Command methods
        [CanExecute(nameof(CopyCommand))]
        private bool CanExecuteCopyCommand(object parameter)
        {
            return CommonViewModel.SelectedElements != null
                    && (CommonViewModel.SelectedElements.Any() && !IsDriveSelected()
                    ? true
                    : false);
        }

        [Execute(nameof(CopyCommand))]
        private async void ExecuteCopyCommand(object parameter)
        {
            var paths = new List<string>();

            foreach (var t in CommonViewModel.SelectedElements)
            {
                paths.Add(t.Path);
            }

            await Task.Run(() => Clipboard.SetFiles(paths, ClipboardFilesState.Copied));
        }

        [CanExecute(nameof(PasteCommand))]
        private bool CanExecutePasteCommand(object parameter)
        {
            return Clipboard.ContainsFiles();
        }

        [Execute(nameof(PasteCommand))]
        private async void ExecutePasteCommand(object parameter)
        {
            var paths = Clipboard.GetFiles();
            string directoryPath;

            if (CommonViewModel.CurrentlyOpenedElement.Path.EndsWith("\\")) directoryPath = CommonViewModel.CurrentlyOpenedElement.Path;
            else directoryPath = CommonViewModel.CurrentlyOpenedElement.Path + "\\";

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

            Clipboard.Clear();

            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }

        [CanExecute(nameof(CutCommand))]
        private bool CanExecuteCutCommand(object parameter)
        {
            return CommonViewModel.SelectedElements != null
                && CommonViewModel.SelectedElements.Any() && !IsDriveSelected();
        }

        [Execute(nameof(CutCommand))]
        private async void ExecuteCutCommand(object parameter)
        {
            var paths = new List<string>();

            foreach (var t in CommonViewModel.SelectedElements)
            {
                paths.Add(t.Path);
            }

            await Task.Run(() => Clipboard.SetFiles(paths, ClipboardFilesState.Cut));
        }

        [CanExecute(nameof(CopyPathCommand))]
        private bool CanExecuteCopyPathCommand(object parameter)
        {
            return CommonViewModel.SelectedElements != null
                && CommonViewModel.SelectedElements.Any();
        }

        [Execute(nameof(CopyPathCommand))]
        private void ExecuteCopyPathCommand(object parameter)
        {
            var text = "";

            if (CommonViewModel.SelectedElements.Count == 1) text = CommonViewModel.SelectedElements[0].Path;
            else
            {
                foreach (var t in CommonViewModel.SelectedElements)
                {
                    text += t.Path + "\n";
                }
            }

            Clipboard.SetText(text);
        }

        [CanExecute(nameof(RemoveCommand))]
        private bool CanExecuteRemoveCommand(object parameter)
        {
            return CommonViewModel.SelectedElements != null
                && CommonViewModel.SelectedElements.Any() 
                && !IsDriveSelected();
        }

        [Execute(nameof(RemoveCommand))]
        private async void ExecuteRemoveCommand(object parameter)
        {
            foreach (var t in CommonViewModel.SelectedElements)
            {
                if (t is PictureLibraryModel.Model.File)
                {
                    _fileService.Remove(t.Path);
                }
                else if (t is PictureLibraryModel.Model.Directory)
                {
                    _directoryService.Remove(t.Path);
                }
            }

            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }

        [CanExecute(nameof(RenameCommand))]
        private bool CanExecuteRenameCommand(object parameter)
        {
            return CommonViewModel.SelectedElements != null
                && CommonViewModel.SelectedElements.Any() && !IsDriveSelected();
        }

        [Execute(nameof(RenameCommand))]
        private void ExecuteRenameCommand(object parameter)
        {
            throw new NotImplementedException();
        }

        [CanExecute(nameof(CreateFolderCommand))]
        private bool CanExecuteCreateFolderCommand(object parameter)
        {
            return (parameter as string) != null
                && (parameter as string).Trim() != String.Empty;
        }

        [Execute(nameof (CreateFolderCommand))]
        private void ExecuteCreateFolderCommand(object parameter)
        {
            throw new NotImplementedException();
        }

        [CanExecute(nameof(BackCommand))]
        private bool CanExecuteBackCommand(object parameter)
        {
            return CommonViewModel.ExplorerHistory.BackStack.Count > 1;
        }

        [Execute(nameof(BackCommand))]
        private void ExecuteBackCommand(object parameter)
        {
            CommonViewModel.Back();
        }

        [CanExecute(nameof(ForwardCommand))]
        private bool CanExecuteForwardCommand(object parameter)
        {
            return CommonViewModel.ExplorerHistory.ForwardStack.Count > 0;
        }

        [Execute(nameof(ForwardCommand))]
        private void ExecuteForwardCommand(object parameter)
        {
            CommonViewModel.Forward();
        }

        [CanExecute(nameof(GoToParentDirectoryCommand))]
        private bool CanExecuteGoToParentDirectoryCommand(object parameter)
        {
            if (CommonViewModel.CurrentlyOpenedElement == null) 
                return false;
            
            return _directoryService.GetParent(CommonViewModel.CurrentlyOpenedElement.Path) != null;
        }

        [Execute(nameof(GoToParentDirectoryCommand))]
        private void ExecuteGoToParentDictionaryCommand(object parameter)
        {
            var parent = _directoryService.GetParent(CommonViewModel.CurrentlyOpenedElement.Path);

            if (parent != null)
                CommonViewModel.CurrentlyOpenedElement = parent;
        }

        [Execute(nameof(RefreshCommand))]
        private async void ExecuteRefreshCommand(object parameter)
        {
            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }
        
        #endregion
    }
}
