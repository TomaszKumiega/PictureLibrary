using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryWPF.CustomControls
{
    public class LibraryExplorerControlsFactory : ILibraryExplorerControlsFactory
    {
        private ILibraryExplorerViewModelFactory ViewModelFactory { get; }
        private IDialogFactory DialogFactory { get; }

        public LibraryExplorerControlsFactory(ILibraryExplorerViewModelFactory viewModelFactory, IDialogFactory dialogFactory)
        {
            ViewModelFactory = viewModelFactory;
            DialogFactory = dialogFactory;
        }


        public async Task<ElementsTree> GetLibrariesTree()
        {
            return new ElementsTree(await ViewModelFactory.GetLibraryTreeViewModelAsync());
        }

        public ElementsView GetLibrariesView()
        {
            return new ElementsView(ViewModelFactory.GetLibraryViewViewModel());
        }

        public LibraryExplorerToolbar GetLibraryExplorerToolbar()
        {
            return new LibraryExplorerToolbar(ViewModelFactory.GetLibraryExplorerToolboxViewModel(new WPFClipboard()), DialogFactory);
        }

        public TagPanel GetTagPanel()
        {
            return new TagPanel(ViewModelFactory.GetTagPanelViewModel());
        }
    }
}
