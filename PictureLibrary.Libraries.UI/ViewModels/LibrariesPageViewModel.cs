using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using PictureLibrary.DataAccess.LibrariesProvider;
using PictureLibrary.Libraries.UI.DataViewModels;
using PictureLibraryModel.Model;
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

        #region Initialization
        [ObservableProperty]
        private bool _initializationInProgress;

        private async Task InitializeViewModelAsync()
        {
            InitializationInProgress = true;

            var libraries = await _librariesProvider.GetLibrariesFromAllSourcesAsync(out var connectionErrors);
            Libraries = new ObservableCollection<LibraryViewModel>(libraries.Select(x => new LibraryViewModel(x)));

            InitializationInProgress = false;
        }
        #endregion
    }
}
