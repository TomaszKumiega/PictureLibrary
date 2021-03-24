using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    public interface IFileExplorerControlsFactory
    {
        Task<ElementsTree> GetFileElementsTreeAsync();
        ElementsView GetFileElementsView();
        FileExplorerToolbar GetFileExplorerToolbar();
    }
}
