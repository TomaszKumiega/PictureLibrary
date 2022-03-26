using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;

namespace PictureLibraryWPF.CustomControls
{
    public class LibraryTree : ElementsTree
    {
        public LibraryTree(ILibraryTreeViewModel viewModel) : base(viewModel)
        {
        }
    }
}
