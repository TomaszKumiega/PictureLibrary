using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public interface ILibrariesViewModel
    {
        /// <summary>
        /// Contains all found libraries
        /// </summary>
        ObservableCollection<Library> Libraries { get; }
        /// <summary>
        /// Contains all library entities that should be displayed 
        /// </summary>
        ObservableCollection<ILibraryEntity> DisplayedEntities { get; }
    }
}
