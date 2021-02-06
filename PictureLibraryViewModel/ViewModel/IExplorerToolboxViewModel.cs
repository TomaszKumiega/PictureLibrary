using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerToolboxViewModel
    {
        IExplorerViewModel CommonViewModel { get; }
        IClipboardService Clipboard { get; }
        ICommand CopyCommand { get; }
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        ICommand CopyPathCommand { get; }
        ICommand RemoveCommand { get; }
        ICommand RenameCommand { get; }
        ICommand RefreshCommand { get; }
        string SearchText { get; set; }

        void Copy();
        void Paste();
        void CopyPath();
        void Remove();
        void Rename();
        void Refresh();
    }
}
