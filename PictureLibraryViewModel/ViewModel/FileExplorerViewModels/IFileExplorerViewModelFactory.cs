using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerViewModelFactory
    {
        IExplorableElementsViewViewModel GetFilesViewViewModel();
        Task<IExplorableElementsTreeViewModel> GetFileTreeViewModelAsync();
        IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard);
    }
}
