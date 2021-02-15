using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryViewViewModel : IExplorableElementsViewViewModel
    {
        public IExplorerViewModel CommonViewModel { get; }

        public LibraryViewViewModel(ILibraryExplorerViewModel commonVM)
        {
            CommonViewModel = commonVM;
        }
    }
}
