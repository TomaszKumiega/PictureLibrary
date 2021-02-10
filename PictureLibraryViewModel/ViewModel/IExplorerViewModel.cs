using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerViewModel : INotifyPropertyChanged
    {
        string CurrentlyOpenedPath { get; set; }
        ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        IDirectoryService DirectoryService { get; }
        string InfoText { get; set; }
        bool IsProcessing { get; set; }

        Task LoadCurrentlyShownElements();
    }
}
