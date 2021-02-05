using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerViewModelFactory
    {
        IFilesViewViewModel GetFilesViewViewModel();
        IFileTreeViewModel GetFileTreeViewModel();
        IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard);
    }
}
