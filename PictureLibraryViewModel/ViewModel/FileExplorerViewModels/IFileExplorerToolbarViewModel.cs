using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerToolbarViewModel : IExplorerToolboxViewModel
    {
        ICommand CreateFolderCommand { get; }
        ICommand GoToParentDirectoryCommand { get; }
        ICommand BackCommand { get; }
        ICommand ForwardCommand { get; }
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        ICommand CopyPathCommand { get; }
        ICommand CopyCommand { get; }
    }
}
