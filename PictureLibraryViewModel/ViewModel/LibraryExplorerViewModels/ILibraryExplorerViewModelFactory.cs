using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public interface ILibraryExplorerViewModelFactory
    {
        ILibraryExplorerToolboxViewModel GetLibraryExplorerToolboxViewModel(IClipboardService clipboard);
        Task<IExplorableElementsTreeViewModel> GetLibraryTreeViewModel();
        IExplorableElementsViewViewModel GetLibraryViewViewModel();
        ITagPanelViewModel GetTagPanelViewModel();
    }
}
