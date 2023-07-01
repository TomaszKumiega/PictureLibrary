using CommunityToolkit.Mvvm.ComponentModel;
using PictureLibrary.DataAccess;
using PictureLibrary.Libraries.UI.DataViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    [QueryProperty(nameof(LibraryId), nameof(LibraryId))]
    public partial class AllTagsPageViewModel : ObservableObject
    {
        private readonly ITagsProvider _tagsProvider;
        private readonly ILibrariesProvider _librariesProvider;

        public AllTagsPageViewModel(
            ITagsProvider tagsProvider,
            ILibrariesProvider librariesProvider)
        {
            _tagsProvider = tagsProvider;
            _librariesProvider = librariesProvider;

            Tags = new ObservableCollection<TagViewModel>();

            PropertyChanged += OnLibraryIdChanged;
        }

        [ObservableProperty]
        private Guid _libraryId;

        public ObservableCollection<TagViewModel> Tags { get; private set; }

        private async void OnLibraryIdChanged(object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(LibraryId))
                return;

            await LoadTags();
        }

        private async Task LoadTags()
        {
            var library = _librariesProvider.GetLibraryFromCacheById(LibraryId) ?? throw new InvalidOperationException();
            var tags = await _tagsProvider.GetTagsFromLibraryAsync(library);

            foreach (var tag in tags)
            {
                Tags.Add(new TagViewModel(tag));
            }
        }
    }
}
