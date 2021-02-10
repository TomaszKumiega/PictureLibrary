using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerToolboxViewModel : IExplorerToolboxViewModel
    {
        ICommand CreateFolderCommand { get; }
        ICommand GoToParentDirectoryCommand { get; }
        ICommand BackCommand { get; }
        ICommand ForwardCommand { get; }

        Task CreateDirectory();
        void GoToParentDirectory();
    }
}
