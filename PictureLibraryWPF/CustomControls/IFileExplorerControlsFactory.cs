using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    public interface IFileExplorerControlsFactory
    {
        ElementsTree GetFileElementsTree();
        ElementsView GetFileElementsView();
        FileExplorerToolbar GetFileExplorerToolbar();
    }
}
