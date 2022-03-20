using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class TagPanelViewModel : ITagPanelViewModel
    {
        public ILibraryExplorerViewModel CommonViewModel { get; }
        public ObservableCollection<Tag> SelectedTags { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }

        public TagPanelViewModel(ILibraryExplorerViewModel commonVM)
        {
            SelectedTags = new ObservableCollection<Tag>();
            Tags = new ObservableCollection<Tag>();
            SelectedTags.CollectionChanged += OnSelectedTagsChanged;
            CommonViewModel = commonVM;
            CommonViewModel.PropertyChanged += OnCurrentlyOpenedElementChanged;
            CommonViewModel.PropertyChanged += OnTagsChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnTagsChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Tags))
            {
                foreach (var t in ((Library)CommonViewModel.CurrentlyOpenedElement).Tags)
                    Tags.Add(t);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tags)));
            }
        }

        private void OnSelectedTagsChanged(object sender, EventArgs args)
        {
            CommonViewModel.LoadCurrentlyShownElements(SelectedTags.ToList());
        }

        private void OnCurrentlyOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(CommonViewModel.CurrentlyOpenedElement)) return;
            if (SelectedTags == null) return;
                SelectedTags.Clear();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tags)));
        }
    }
}
