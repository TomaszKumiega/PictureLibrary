using PictureLibraryModel.Services.Clipboard;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerToolboxViewModel
    { 
        IExplorerViewModel CommonViewModel { get; }
        IClipboardService Clipboard { get; }
        ICommand RemoveCommand { get; }
        ICommand RenameCommand { get; }
        ICommand RefreshCommand { get; }
        string SearchText { get; set; }
    }
}
