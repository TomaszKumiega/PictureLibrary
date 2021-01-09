using PictureLibraryModel.Model;
using System.Collections.ObjectModel;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IFileExplorerViewModel
    {
        ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; }
        string CurrentDirectoryPath { get; set; }
        ObservableCollection<IFileSystemEntity> DirectoryTree { get; }
    }
}