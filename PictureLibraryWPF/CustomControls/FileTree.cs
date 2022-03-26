using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;

namespace PictureLibraryWPF.CustomControls
{
    public class FileTree : ElementsTree
    {
        public FileTree(IFileTreeViewModel viewModel) : base(viewModel)
        {
        }
    }
}
