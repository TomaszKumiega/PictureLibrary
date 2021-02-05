using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FilesViewViewModel : IFilesViewViewModel
    {
        public IFileExplorerViewModel CommonViewModel { get; }

        public FilesViewViewModel(IFileExplorerViewModel viewModel)
        {
            CommonViewModel = viewModel;
        }
    }
}
