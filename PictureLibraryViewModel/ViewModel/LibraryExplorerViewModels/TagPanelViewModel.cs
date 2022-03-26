using PictureLibraryModel.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class TagPanelViewModel : ITagPanelViewModel
    {
        #region Public properties
        public ILibraryExplorerViewModel CommonViewModel { get; }
        public ObservableCollection<Tag> SelectedTags { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public TagPanelViewModel(ILibraryExplorerViewModel commonVM)
        {
            SelectedTags = new ObservableCollection<Tag>();
            Tags = new ObservableCollection<Tag>();
            CommonViewModel = commonVM;

            SelectedTags.CollectionChanged += OnSelectedTagsChanged;
            CommonViewModel.PropertyChanged += OnCurrentlyOpenedElementChanged;
            CommonViewModel.PropertyChanged += OnTagsChanged;
        }

        #region Event handlers
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
        #endregion
    }
}
