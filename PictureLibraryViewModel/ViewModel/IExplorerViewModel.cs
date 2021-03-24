using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Helpers;
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
        IExplorableElement CurrentlyOpenedElement { get; set; }
        ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        string InfoText { get; set; }
        bool IsProcessing { get; set; }
        IExplorerHistory ExplorerHistory { get; }

        void Back();
        void Forward();
        Task LoadCurrentlyShownElementsAsync();
    }
}
