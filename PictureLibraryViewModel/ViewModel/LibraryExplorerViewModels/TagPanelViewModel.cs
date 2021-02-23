using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class TagPanelViewModel : ITagPanelViewModel
    {
        public ILibraryExplorerViewModel CommonViewModel { get; }
        public ObservableCollection<Tag> SelectedTags { get; }

        public TagPanelViewModel(ILibraryExplorerViewModel commonVM)
        {
            SelectedTags = new ObservableCollection<Tag>();
            SelectedTags.CollectionChanged += OnSelectedTagsChanged;
            CommonViewModel = commonVM;
            CommonViewModel.PropertyChanged += OnCurrentlyOpenedElementChanged;
        }

        private async void OnSelectedTagsChanged(object sender, EventArgs args)
        {
            await CommonViewModel.LoadCurrentlyShownElements(SelectedTags.ToList());
        }

        private void OnCurrentlyOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != "CurrentlyOpenedElement") return;
            if (SelectedTags == null) return;
            SelectedTags.Clear();
        }
    }
}
