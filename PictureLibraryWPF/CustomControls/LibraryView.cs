using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;

namespace PictureLibraryWPF.CustomControls
{
    public class LibraryView : ElementsView
    {
        public LibraryView(ILibraryViewViewModel viewModel, Func<TagPanel> tagPanelLocator) : base(viewModel, tagPanelLocator)
        {
        }
    }
}
