using PictureLibraryModel.Model;
using System.Collections.ObjectModel;

namespace PictureLibraryViewModel.ViewModels
{
    public interface IFileSystemViewModel
    {
        /// <summary>
        /// Contains a path of directory object is currently operating on
        /// </summary>
        string CurrentDirectory { get; set; }
        /// <summary>
        /// Containes drives from computers filesystem
        /// </summary>
        ObservableCollection<Drive> Drives { get; }
       
    }
}
