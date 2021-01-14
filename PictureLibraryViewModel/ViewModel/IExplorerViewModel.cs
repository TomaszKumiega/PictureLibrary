using PictureLibraryModel.Model;
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
        IExplorableElement CopiedFile { get; set; }
        IExplorableElement CutFile { get; set; }
        void Copy();
        void Cut();
        void Paste();
    }
}
