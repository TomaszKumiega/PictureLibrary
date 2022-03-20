using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryWPF.CustomControls
{
    public interface ILibraryExplorerControlsFactory
    {
        Task<ElementsTree> GetLibrariesTreeAsync();
        Task<ElementsView> GetLibrariesViewAsync();
    }
}
