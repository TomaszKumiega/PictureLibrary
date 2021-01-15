using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IFileExplorerViewModel : IExplorerViewModel
    {
        ObservableCollection<IExplorableElement> CurrentDirectoryFiles { get; }
        string CurrentDirectoryPath { get; set; }
        ObservableCollection<IExplorableElement> DirectoryTree { get; }
        ICommand CopyPathCommand { get; }
        
        void CopyPath();
    }
}