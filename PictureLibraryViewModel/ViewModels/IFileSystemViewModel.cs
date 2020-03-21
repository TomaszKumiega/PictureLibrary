using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public interface IFileSystemViewModel
    {
        /// <summary>
        /// Contains a path of directory object is currently operating on
        /// </summary>
        string CurrentDirectory { get; }
        /// <summary>
        /// Containes drives from computers filesystem
        /// </summary>
        ObservableCollection<Drive> Drives { get; }
       
    }
}
