using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerToolboxViewModel
    {
        IFileExplorerViewModel FileExplorerViewModel { get; }
        IClipboardService Clipboard { get; }
        ICommand CopyCommand { get; }
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        ICommand CopyPathCommand { get; }
        ICommand RemoveCommand { get; }
        ICommand RenameCommand { get; }
        ICommand CreateFolderCommand { get; }
        ICommand BackCommand { get; }
        ICommand ForwardCommand { get; }
        ICommand GoToParentDirectoryCommand { get; }
        ICommand RefreshCommand { get; }
        string SearchText { get; set; }

        void Copy();
        void Paste();
        void CopyPath();
        void Remove();
        void Rename();
        void CreateDirectory();
        void GoToParentDirectory();
        void Refresh();
    }
}
