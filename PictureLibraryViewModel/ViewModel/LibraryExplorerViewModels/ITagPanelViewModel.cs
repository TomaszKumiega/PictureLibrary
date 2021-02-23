using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public interface ITagPanelViewModel
    {
        ILibraryExplorerViewModel CommonViewModel { get; }
        ObservableCollection<Tag> SelectedTags { get; }
    }
}
