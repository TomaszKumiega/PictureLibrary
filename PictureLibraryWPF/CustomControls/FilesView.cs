using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;

namespace PictureLibraryWPF.CustomControls
{
    public class FilesView : ElementsView
    {
        public FilesView(IFilesViewViewModel viewModel) : base(viewModel)
        {
        }
    }
}
