using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IFileExplorerViewModel : IExplorerViewModel
    {
        string CurrentDirectoryPath { get; set; }
        ICommand CopyPathCommand { get; }
        
        void CopyPath();
    }
}