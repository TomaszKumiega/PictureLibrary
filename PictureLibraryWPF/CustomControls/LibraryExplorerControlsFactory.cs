using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryWPF.CustomControls
{
    public class LibraryExplorerControlsFactory : ILibraryExplorerControlsFactory
    {
        public ILibraryExplorerViewModelFactory _viewModelFactory;

        public LibraryExplorerControlsFactory(ILibraryExplorerViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }


        public ElementsTree GetLibrariesTree()
        {
            return new ElementsTree(_viewModelFactory.GetLibraryTreeViewModel());
        }

        public ElementsView GetLibrariesView()
        {
            return new ElementsView(_viewModelFactory.GetLibraryViewViewModel());
        }

        public LibraryExplorerToolbar GetLibraryExplorerToolbar()
        {
            return new LibraryExplorerToolbar(_viewModelFactory.GetLibraryExplorerToolboxViewModel(new WPFClipboard()));
        }

        public TagPanel GetTagPanel()
        {
            return new TagPanel(_viewModelFactory.GetTagPanelViewModel());
        }
    }
}
