using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorableElementsViewViewModel
    {
        IExplorerViewModel CommonViewModel { get; }
    }
}
