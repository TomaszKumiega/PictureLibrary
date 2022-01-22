using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.Dialogs;
using System;
using System.Threading.Tasks;

namespace PictureLibraryWPF.CustomControls
{
    public class LibraryExplorerControlsFactory : ILibraryExplorerControlsFactory
    {
        private ILibraryExplorerViewModelFactory ViewModelFactory { get; } 
        private Func<TagPanel> TagPanelLocator { get; }

        public LibraryExplorerControlsFactory(ILibraryExplorerViewModelFactory viewModelFactory, Func<TagPanel> tagPanelLocator)
        {
            ViewModelFactory = viewModelFactory;
            TagPanelLocator = tagPanelLocator;
        }

        public async Task<ElementsTree> GetLibrariesTreeAsync()
        {
            return new ElementsTree(await ViewModelFactory.GetLibraryTreeViewModelAsync());
        }

        public async Task<ElementsView> GetLibrariesViewAsync()
        {
            var viewModel = new ElementsView(await ViewModelFactory.GetLibraryViewViewModelAsync(), TagPanelLocator);
            return viewModel;
        }

        public TagPanel GetTagPanel()
        {
            return new TagPanel(ViewModelFactory.GetTagPanelViewModel());
        }
    }
}
