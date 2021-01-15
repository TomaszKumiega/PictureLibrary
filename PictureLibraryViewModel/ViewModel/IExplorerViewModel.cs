using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerViewModel : INotifyPropertyChanged
    {
        List<IExplorableElement> SelectedFiles { get; set; }
        IExplorableElement SelectedNode { get; set; }
        ICommand CopyFileCommand { get; }
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        IClipboardService Clipboard { get; }
        
        void Copy();
        void Cut();
        void Paste();
    }
}
