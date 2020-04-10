using PictureLibraryModel.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PictureLibraryViewModel.ViewModels
{
    public interface IFileSystemViewModel
    {
        /// <summary>
        /// Contains a path of directory object is currently operating on
        /// </summary>
        string CurrentDirectoryPath { get; set; }
        /// <summary>
        /// Currently displayed directory content
        /// </summary>
        ObservableCollection<IFileSystemEntity> CurrentDirectoryContent { get; }
        /// <summary>
        /// Containes drives from computers filesystem
        /// </summary>
        ObservableCollection<Drive> Drives { get; }
    }
}
