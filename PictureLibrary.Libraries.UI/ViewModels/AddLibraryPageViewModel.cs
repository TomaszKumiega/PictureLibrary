using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.LibraryService;
using PictureLibrary.Infrastructure.ImplementationSelector;
using PictureLibrary.Infrastructure.UI;
using PictureLibrary.Libraries.UI.DataViewModels;
using PictureLibraryModel.Builders;
using PictureLibraryModel.Model.DataStoreInfo;
using System.Text;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    public partial class AddLibraryPageViewModel : ObservableObject
    {
        private readonly IPopupService _popupService;
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly Func<ILibraryBuilder> _libraryBuilderLocator;
        private readonly IImplementationSelector<DataStoreType, ILibraryService> _libraryServiceSelector;

        public AddLibraryPageViewModel(
            IPopupService popupService,
            IDataStoreInfoService dataStoreInfoService,
            Func<ILibraryBuilder> libraryBuilderLocator,
            IImplementationSelector<DataStoreType, ILibraryService> libraryServiceSelector)
        {
            _popupService = popupService;
            _dataStoreInfoService = dataStoreInfoService;
            _libraryBuilderLocator = libraryBuilderLocator;
            _libraryServiceSelector = libraryServiceSelector;
        }

        public IEnumerable<DataStoreInfoItemViewModel> DataStoreInfos
        {
            get
            {
                var dataStoreInfos = _dataStoreInfoService.GetAllDataStoreInfos();

                var dataStoreInfoItems = new List<DataStoreInfoItemViewModel>()
                {
                    new DataStoreInfoItemViewModel(null),                    
                };

                dataStoreInfoItems.AddRange(dataStoreInfos.Select(x => new DataStoreInfoItemViewModel(x)));

                return dataStoreInfoItems;
            }
        }

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private DataStoreInfoItemViewModel? _dataStoreInfo;

        #region Validation
        private IEnumerable<string> GetValidationErrors()
        {
            if (string.IsNullOrEmpty(Name))
                yield return "Name must not be empty";
        }

        private bool IsValid()
        {
            var validationErrors = GetValidationErrors();

            if (validationErrors?.Any() == true)
            {
                var errorStringBuilder = new StringBuilder();

                foreach (var error in validationErrors)
                {
                    errorStringBuilder.AppendLine($"- {error}");
                }

                _popupService.DisplayAlert("Wystąpiły błędy", errorStringBuilder.ToString(), "OK");

                return false;
            }

            return true;
        }
        #endregion

        #region Commands

        [RelayCommand]
        public async Task Add()
        {
            if (!IsValid())
                return;

            var dataStoreType = DataStoreInfo?.DataStoreInfo?.Type ?? DataStoreType.Local;

            var service = _libraryServiceSelector.Select(dataStoreType) ?? throw new InvalidOperationException("Selected data storage is not supported");

            var library = _libraryBuilderLocator()
                .CreateLibrary(DataStoreInfo?.DataStoreInfo)
                .WithName(Name!)
                .WithDescription(Description!)
                .GetLibrary();

            await service.AddLibraryAsync(library);

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
