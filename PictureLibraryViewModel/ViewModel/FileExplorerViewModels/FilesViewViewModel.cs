using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FilesViewViewModel : IExplorableElementsViewViewModel
    {
        public IExplorerViewModel CommonViewModel { get; }

        public FilesViewViewModel(IFileExplorerViewModel viewModel)
        {
            CommonViewModel = viewModel;
        }
    }
}
