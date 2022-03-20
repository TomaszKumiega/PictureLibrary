using PictureLibraryModel.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public interface ITagPanelViewModel : INotifyPropertyChanged
    {
        ILibraryExplorerViewModel CommonViewModel { get; }
        ObservableCollection<Tag> SelectedTags { get; set; }
        ObservableCollection<Tag> Tags { get; }
    }
}
