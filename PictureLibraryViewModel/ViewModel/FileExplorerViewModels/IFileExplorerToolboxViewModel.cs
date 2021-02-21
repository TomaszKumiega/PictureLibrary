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
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        ICommand CopyPathCommand { get; }
        ICommand CopyCommand { get; }

        Task CreateDirectory();
        void GoToParentDirectory();
        Task Paste();
        Task CopyPath();
        Task Cut();
        Task Copy();
    }
}
