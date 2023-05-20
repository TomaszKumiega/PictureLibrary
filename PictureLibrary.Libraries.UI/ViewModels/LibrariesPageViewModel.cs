using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibrary.DataAccess;
using PictureLibrary.Libraries.UI.DataViewModels;
using PictureLibrary.Libraries.UI.Pages;
using System.Collections.ObjectModel;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    public partial class LibrariesPageViewModel : ObservableObject
    {
        private readonly ILibrariesProvider _librariesProvider;

        public LibrariesPageViewModel(ILibrariesProvider librariesProvider)
        {
            _librariesProvider = librariesProvider;

            InitializeViewModelAsync().SafeFireAndForget();
        }

        public ObservableCollection<LibraryViewModel> Libraries { get; private set; }

        [ObservableProperty]
        private LibraryViewModel _selectedLibrary;

        #region Initialization
        [ObservableProperty]
        private bool _initializationInProgress;

        private async Task InitializeViewModelAsync()
        {
            InitializationInProgress = true;

            var libraries = await _librariesProvider.GetLibrariesFromAllSourcesAsync();
            Libraries = new ObservableCollection<LibraryViewModel>(libraries.Select(x => new LibraryViewModel(x)));

            InitializationInProgress = false;
        }
        #endregion

        #region Commands

        private bool CanExecuteOpenLibrary()
        {
            return SelectedLibrary != null;
        }

        [RelayCommand(CanExecute = nameof(CanExecuteOpenLibrary))]
        public async Task OpenLibrary()
        {
            await Shell.Current.GoToAsync(nameof(LibraryContentPage), new Dictionary<string, object>
            {
                { nameof(LibraryViewModel), SelectedLibrary },
            });
        }
        #endregion
    }
}
