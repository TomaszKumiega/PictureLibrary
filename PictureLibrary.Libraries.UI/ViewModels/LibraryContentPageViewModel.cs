using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibrary.DataAccess;
using PictureLibrary.Libraries.UI.DataViewModels;
using PictureLibrary.Libraries.UI.Pages;
using PictureLibraryModel.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    [QueryProperty(nameof(LibraryId), nameof(LibraryId))]
    public partial class LibraryContentPageViewModel : ObservableObject
    {
        private readonly ITagsProvider _tagsProvider;
        private readonly ILibrariesProvider _librariesProvider;

        public LibraryContentPageViewModel(
            ITagsProvider tagsProvider,
            ILibrariesProvider librariesProvider)
        {
            _tagsProvider = tagsProvider;
            _librariesProvider = librariesProvider;
            Tags = new ObservableCollection<TagViewModel>();

            PropertyChanged += LibraryChanged;
        }

        public Guid LibraryId
        {
            set
            {
                Library = _librariesProvider.GetLibraryFromCacheById(value) ?? throw new InvalidOperationException("Library not found");
            }
        }

        [ObservableProperty]
        private Library? _library;

        public ObservableCollection<TagViewModel> Tags { get; }

        private async void LibraryChanged(object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(Library))
                return;

            await LoadTagsAsync();
        }

        #region Tags initialization
        private async Task LoadTagsAsync()
        {
            if (Library == null)
                return;

            Tags.Clear();

            var tags = await _tagsProvider.GetTagsFromLibraryAsync(Library);

            foreach (var tag in tags.Select(x => new TagViewModel(x)))
            {
                Tags.Add(tag);
            }

            Tags.Add(new TagViewModel(AddTagCommand, "+", Colors.Transparent));
            Tags.Add(new TagViewModel(ShowAllTagsCommand, "Pokaż wszystkie tagi", Colors.Transparent));
        }
        #endregion

        #region Commands
        [RelayCommand]
        private Task ShowAllTags()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task AddTag()
        {
            await Shell.Current.GoToAsync(nameof(AddTagPage), new Dictionary<string, object>
            {
                { nameof(AddTagPageViewModel.LibraryId), Library?.Id ?? Guid.Empty},
            });
        }
        #endregion
    }
}
