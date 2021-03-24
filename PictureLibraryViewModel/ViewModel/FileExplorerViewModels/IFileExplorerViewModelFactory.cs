using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerViewModelFactory
    {
        Task<IExplorableElementsViewViewModel> GetFilesViewViewModelAsync();
        Task<IExplorableElementsTreeViewModel> GetFileTreeViewModelAsync();
        IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard);
    }
}
