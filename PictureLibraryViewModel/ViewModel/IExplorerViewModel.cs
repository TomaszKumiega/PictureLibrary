using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerViewModel
    {
        IExplorableElement SelectedFile { get; set; }
        IExplorableElement SelectedNode { get; set; }
        ICommand CopyFileCommand { get; }
        ICommand PasteCommand { get; }
        IClipboardService Clipboard { get; }
        
        void Copy();
        void Cut();
        void Paste();
        void CopyPath();
    }
}
