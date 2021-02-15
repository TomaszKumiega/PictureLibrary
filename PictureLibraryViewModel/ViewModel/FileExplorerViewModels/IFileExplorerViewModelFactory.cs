using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerViewModelFactory
    {
        IExplorableElementsViewViewModel GetFilesViewViewModel();
        IExplorableElementsTreeViewModel GetFileTreeViewModel();
        IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard);
    }
}
