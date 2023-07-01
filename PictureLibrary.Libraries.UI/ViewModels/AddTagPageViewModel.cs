using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibrary.DataAccess;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.TagService;
using PictureLibrary.Infrastructure.ImplementationSelector;
using PictureLibrary.Libraries.UI.MainPage;
using PictureLibrary.Libraries.UI.Pages;
using PictureLibraryModel.Builders;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    [QueryProperty(nameof(LibraryId), nameof(LibraryId))]
    public partial class AddTagPageViewModel : ObservableObject
    {
        private readonly Func<ITagBuilder> _tagBuilderLocator;
        private readonly ILibrariesProvider _librariesProvider;
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly IImplementationSelector<DataStoreType, ITagService> _tagServiceSelector;

        public AddTagPageViewModel(
            Func<ITagBuilder> tagBuilderLocator,
            ILibrariesProvider librariesProvider,
            IDataStoreInfoService dataStoreInfoService,
            IImplementationSelector<DataStoreType, ITagService> tagServiceSelector)
        {
            _tagBuilderLocator = tagBuilderLocator;
            _librariesProvider = librariesProvider;
            _tagServiceSelector = tagServiceSelector;
            _dataStoreInfoService = dataStoreInfoService;
        }

        #region Public properties
        [ObservableProperty]
        private Guid _libraryId;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _description;
        #endregion

        #region Validation
        private bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }
        #endregion

        #region Commands
        [RelayCommand]
        public async Task Add()
        {
            if (!IsValid())
                return;

            var library = _librariesProvider.GetLibraryFromCacheById(LibraryId) ?? throw new InvalidOperationException("Library doesn't exist.");
            var dataStoreType = _dataStoreInfoService.GetDataStoreInfoFromLibrary(library)?.Type ?? DataStoreType.Local;

            var tag = _tagBuilderLocator()
                .CreateTag()
                .WithName(Name!)
                .WithDescription(Description)
                .WithColor(Colors.Transparent.ToArgbHex())
                .GetTag();

            var tagService = _tagServiceSelector.Select(dataStoreType);

            await tagService.AddTagAsync(library, tag);

            await Shell.Current.GoToAsync($"..", new Dictionary<string, object>
            {
                { nameof(LibraryContentPageViewModel.LibraryId), LibraryId }
            });
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..", new Dictionary<string, object>
            {
                { nameof(LibraryContentPageViewModel.LibraryId), LibraryId }
            });
        }
        #endregion
    }
}
