using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerViewModel
    {
        IExplorableElement SelectedFile { get; set; }
        IExplorableElement SelectedNode { get; set; }
        CopyFileCommand CopyFileCommand { get; set; }
        void CopyFile();
    }
}
