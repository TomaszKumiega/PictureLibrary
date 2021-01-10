using PictureLibraryModel.Model;
using System.Collections.ObjectModel;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IFileExplorerViewModel
    {
        ObservableCollection<IFileSystemEntity> CurrentDirectoryFiles { get; }
        string CurrentDirectoryPath { get; set; }
        ObservableCollection<IFileSystemEntity> DirectoryTree { get; }
        /// <summary>
        /// File selected on the pain panel
        /// </summary>
        IFileSystemEntity SelectedFile { get; set; }
        /// <summary>
        /// Node selected on directory tree
        /// </summary>
        IFileSystemEntity SelectedNode { get; set; }

        void CopyFile();
    }
}