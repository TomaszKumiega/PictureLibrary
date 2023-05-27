using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibrary.DataAccess;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.LibraryService;
using PictureLibrary.Infrastructure.ImplementationSelector;
using PictureLibrary.Infrastructure.UI;
using PictureLibrary.Libraries.UI.DataViewModels;
using PictureLibrary.Libraries.UI.Pages;
using PictureLibraryModel.Model.DataStoreInfo;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    [QueryProperty(nameof(NewLibraryId), nameof(NewLibraryId))]
    public partial class LibrariesPageViewModel : ObservableObject
    {
        private readonly IPopupService _popupService;
        private readonly ILibrariesProvider _librariesProvider;
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly IImplementationSelector<DataStoreType, ILibraryService> _libraryServiceSelector;

        public LibrariesPageViewModel(
            IPopupService popupService,
            ILibrariesProvider librariesProvider,
            IDataStoreInfoService dataStoreInfoService,
            IImplementationSelector<DataStoreType, ILibraryService> libraryServiceSelector)
        {
            _popupService = popupService;
            _librariesProvider = librariesProvider;
            _dataStoreInfoService = dataStoreInfoService;
            _libraryServiceSelector = libraryServiceSelector;

            Libraries = new ObservableCollection<LibraryViewModel>();

            PropertyChanged += PropertyChangedHandler;

            InitializeViewModelAsync().SafeFireAndForget();
        }

        #region Public Properties
        public ObservableCollection<LibraryViewModel> Libraries { get; }

        public Guid NewLibraryId
        {
            set
            {
                var newLibrary = _librariesProvider.GetLibraryFromCacheById(value);
                
                if (newLibrary != null)
                {
                    Libraries.Add(new LibraryViewModel(newLibrary));
                }
            }
        }

        [ObservableProperty]
        private LibraryViewModel _selectedLibrary;
        #endregion

        #region Initialization
        [ObservableProperty]
        private bool _initializationInProgress;

        private async Task InitializeViewModelAsync()
        {
            InitializationInProgress = true;

            var libraries = await _librariesProvider.GetLibrariesFromAllSourcesAsync();
            
            foreach (var library in libraries.Select(x => new LibraryViewModel(x)))
            {
                Libraries.Add(library);
            }

            InitializationInProgress = false;
        }
        #endregion

        #region Commands

        private void NotifyCanExecuteChanged()
        {
            OpenLibraryCommand.NotifyCanExecuteChanged();
            RemoveLibraryCommand.NotifyCanExecuteChanged();
        }


        private bool CanExecuteItemSelected()
        {
            return SelectedLibrary != null;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteItemSelected))]
        public async Task OpenLibrary()
        {
            await Shell.Current.GoToAsync(nameof(LibraryContentPage), new Dictionary<string, object>
            {
                { nameof(LibraryViewModel), SelectedLibrary },
            });
        }

        [RelayCommand]
        public async Task AddLibrary()
        {
            await Shell.Current.GoToAsync(nameof(AddLibraryPage));
        }

        [RelayCommand(CanExecute = nameof(CanExecuteItemSelected))]
        public async Task RemoveLibrary()
        {
            var dataStoreInfo = _dataStoreInfoService.GetDataStoreInfoFromLibrary(SelectedLibrary.Library);
            var dataStoreType = dataStoreInfo?.Type ?? DataStoreType.Local;
            var libraryService = _libraryServiceSelector.Select(dataStoreType);

            bool libraryDeleted = await libraryService.DeleteLibraryAsync(SelectedLibrary.Library);

            if (!libraryDeleted)
            {
                _popupService.DisplayAlert("Błąd", "Usuwanie biblioteki nie powiodło się.", "Ok");
                return;
            }

            Libraries.Remove(SelectedLibrary);
        }
        #endregion

        #region OnPropertyChanged
        public void PropertyChangedHandler(object? sender, PropertyChangedEventArgs args)
        {
            NotifyCanExecuteChanged();
        }
        #endregion
    }
}
