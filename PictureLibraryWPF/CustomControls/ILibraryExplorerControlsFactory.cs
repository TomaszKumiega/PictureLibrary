using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryWPF.CustomControls
{
    public interface ILibraryExplorerControlsFactory
    {
        ElementsTree GetLibrariesTree();
        ElementsView GetLibrariesView();
        LibraryExplorerToolbar GetLibraryExplorerToolbar();
        TagPanel GetTagPanel();
    }
}
