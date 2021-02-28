using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public interface ILibraryExplorerViewModelFactory
    {
        ILibraryExplorerToolboxViewModel GetLibraryExplorerToolboxViewModel(IClipboardService clipboard);
        IExplorableElementsTreeViewModel GetLibraryTreeViewModel();
        IExplorableElementsViewViewModel GetLibraryViewViewModel();
        ITagPanelViewModel GetTagPanelViewModel();
    }
}
