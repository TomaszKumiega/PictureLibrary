using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.Dialogs;
using System;
using System.Threading.Tasks;

namespace PictureLibraryWPF.CustomControls
{
    public class FileExplorerControlsFactory : IFileExplorerControlsFactory
    {
        private IFileExplorerViewModelFactory FileExplorerViewModelFactory { get; }
        private IDialogViewModelFactory DialogViewModelFactory { get; }
        private Func<TagPanel> TagPanelLocator { get; }

        public FileExplorerControlsFactory(IFileExplorerViewModelFactory fileExplorerViewModelFactory, IDialogViewModelFactory dialogViewModelFactory, Func<TagPanel> tagPanelLocator)
        {
            FileExplorerViewModelFactory = fileExplorerViewModelFactory;
            DialogViewModelFactory = dialogViewModelFactory;
            TagPanelLocator = tagPanelLocator;
        }

        public async Task<ElementsTree> GetFileElementsTreeAsync()
        {
            return new ElementsTree(await FileExplorerViewModelFactory.GetFileTreeViewModelAsync());
        }

        public async Task<ElementsView> GetFileElementsViewAsync()
        {
            return new ElementsView(await FileExplorerViewModelFactory.GetFilesViewViewModelAsync(), TagPanelLocator);
        }

        public FileExplorerToolbar GetFileExplorerToolbar()
        {
            return new FileExplorerToolbar(FileExplorerViewModelFactory.GetFileToolboxViewModel(new WPFClipboard()), new DialogFactory(DialogViewModelFactory));
        }
    }
}
