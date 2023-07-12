using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibrary.DataAccess;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.ImageFileService;
using PictureLibrary.FileSystem.API;
using PictureLibrary.Infrastructure.ImplementationSelector;
using PictureLibrary.Libraries.UI.DataViewModels;
using PictureLibrary.Libraries.UI.Pages;
using PictureLibraryModel.Builders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PictureLibrary.Libraries.UI.ViewModels
{
    [QueryProperty(nameof(LibraryId), nameof(LibraryId))]
    public partial class LibraryContentPageViewModel : ObservableObject
    {
        private readonly IFileService _fileService;
        private readonly ITagsProvider _tagsProvider;
        private readonly ILibrariesProvider _librariesProvider;
        private readonly IImageFilesProvider _imageFilesProvider;
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly Func<IImageFileBuilder> _imageFileBuilderLocator;
        private readonly IImplementationSelector<DataStoreType, IImageFileService> _imageFileServiceSelector;

        public LibraryContentPageViewModel(
            IFileService fileService,
            ITagsProvider tagsProvider,
            ILibrariesProvider librariesProvider,
            IImageFilesProvider imageFilesProvider,
            IDataStoreInfoService dataStoreInfoService,
            Func<IImageFileBuilder> imageFileBuilderLocator,
            IImplementationSelector<DataStoreType, IImageFileService> imageFileServiceSelector)
        {
            _fileService = fileService;
            _tagsProvider = tagsProvider;
            _librariesProvider = librariesProvider;
            _imageFilesProvider = imageFilesProvider;
            _dataStoreInfoService = dataStoreInfoService;
            _imageFileBuilderLocator = imageFileBuilderLocator;
            _imageFileServiceSelector = imageFileServiceSelector;

            Tags = new ObservableCollection<TagViewModel>();
            ImageFiles = new ObservableCollection<ImageFileViewModel>();

            PropertyChanged += LibraryChanged;
        }

        public Guid LibraryId
        {
            set
            {
                Library = _librariesProvider.GetLibraryFromCacheById(value) ?? throw new InvalidOperationException("Library not found");
            }
        }

        private Library? _library;
        public Library? Library
        {
            get => _library;
            set
            {
                _library = value;
                OnPropertyChanged(nameof(Library));
            }
        }

        public ObservableCollection<TagViewModel> Tags { get; }
        public ObservableCollection<ImageFileViewModel> ImageFiles { get; }

        private async void LibraryChanged(object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(Library))
                return;

            await LoadTagsAsync();
            await LoadImageFilesAsync();
        }

        #region Tags initialization
        private async Task LoadTagsAsync()
        {
            if (Library == null)
            {
                return;
            }

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

        #region ImageFiles initialization
        private async Task LoadImageFilesAsync()
        {
            if (Library == null)
            {
                return;
            }

            ImageFiles.Clear();

            var imageFiles = await _imageFilesProvider.GetAllImageFilesFromLibraryAsync(Library);

            foreach (var imageFile in imageFiles.Select(x => new ImageFileViewModel(x)))
            {
                ImageFiles.Add(imageFile);
            }
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task ShowAllTags()
        {
            await Shell.Current.GoToAsync(nameof(AllTagsPage), new Dictionary<string, object>
            {
                { nameof(AllTagsPageViewModel.LibraryId), Library?.Id ?? Guid.Empty }
            });
        }

        [RelayCommand]
        private async Task AddTag()
        {
            await Shell.Current.GoToAsync(nameof(AddTagPage), new Dictionary<string, object>
            {
                { nameof(AddTagPageViewModel.LibraryId), Library?.Id ?? Guid.Empty},
            });
        }

        [RelayCommand]
        private async Task AddImageFile()
        {
            if (Library == null)
                return;

            var fileResults = await FilePicker.PickMultipleAsync(PickOptions.Images);

            if (!fileResults.Any())
                return;

            var dataStoreInfo = _dataStoreInfoService.GetDataStoreInfoFromLibrary(Library);

            var dataStoreType = dataStoreInfo == null
                ? DataStoreType.Local
                : dataStoreInfo.Type;
            var imageFileService = _imageFileServiceSelector.Select(dataStoreType);

            foreach (var fileResult in fileResults)
            {
                var fileInfo = _fileService.GetFileInfo(fileResult.FullPath);
                var imageFile = _imageFileBuilderLocator()
                    .CreateImageFile(dataStoreInfo, fileResult.FullPath)
                    .WithName(fileInfo.Name)
                    .WithExtension(fileInfo.Extension)
                    .GetImageFile();

                using var stream = _fileService.Open(fileInfo.FullName);

                await imageFileService.AddImageFile(imageFile, stream, Library);
            }
        }
        #endregion
    }
}
