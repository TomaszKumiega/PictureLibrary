using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddImagesDialogViewModel : IAddImagesDialogViewModel, INotifyPropertyChanged
    {
        #region Private fields
        private readonly ILibraryExplorerViewModel _commonViewModel;
        private readonly IDataSourceCollection _dataSourceCollection;
        #endregion

        #region Public properties
        public List<Library> Libraries { get; }

        private Library _selectedLibrary;
        public Library SelectedLibrary
        {
            get => _selectedLibrary;
            set
            {
                _selectedLibrary = value;
                NotifyPropertyChanged(nameof(SelectedLibrary));
            }
        }
        public Tag SelectedTag
        {
            get => null;
            set
            {
                if (!SelectedTags.Contains(value))
                {
                    SelectedTags.Add(value);
                }    

                NotifyPropertyChanged(nameof(SelectedTag));
            }
        }
        public IEnumerable<ImageFile> SelectedImages { private get; set; }
        public ObservableCollection<Tag> SelectedTags { get; }
        #endregion

        #region Commands
        [Command]
        public ICommand AddImagesCommand { get; set; }
        [Command]
        public ICommand RemoveTagCommand { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        #endregion

        public AddImagesDialogViewModel(
            ILibraryExplorerViewModel commonVM, 
            IDataSourceCollection dataSourceCollection, 
            ICommandCreator commandCreator)
        {
            _commonViewModel = commonVM;
            _dataSourceCollection = dataSourceCollection;

            _dataSourceCollection.Initialize(new List<IRemoteStorageInfo>());
            Libraries = _dataSourceCollection.GetAllLibraries();
            SelectedTags = new ObservableCollection<Tag>();

            commandCreator.InitializeCommands(this);
        }

        #region Command methods
        [CanExecute(nameof(AddImagesCommand))]
        private bool CanExecuteAddImagesCommand(object parameter)
        {
            return SelectedLibrary != null;
        }

        [Execute(nameof(AddImagesCommand))]
        private async void ExecuteAddImagesCommand(object parameter)
        {
            await AddAsync();
        }

        [CanExecute(nameof(RemoveTagCommand))]
        private bool CanExecuteRemoveTagCommand(object parameter)
        {
            return parameter is Tag tag && SelectedTags.Contains(tag);
        }

        [Execute(nameof(RemoveTagCommand))]
        private void ExecuteRemoveTagCommand(object parameter)
        {
            if (parameter is Tag tag)
            {
                SelectedTags.Remove(tag);
                NotifyPropertyChanged(nameof(SelectedTags));
            }
        }
        #endregion

        #region Private methods
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Public methods
        public async Task AddAsync()
        {
            var dataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(SelectedLibrary.RemoteStorageInfoId);

            foreach (var t in SelectedImages)
            {
                await Task.Run(() => dataSource.ImageProvider.AddImageToLibrary(t, SelectedLibrary.Path));
                SelectedLibrary.Images.Add(t);
            }

            await Task.Run(() => dataSource.LibraryProvider.UpdateLibrary(SelectedLibrary));

            await _commonViewModel.LoadCurrentlyShownElementsAsync();
            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
        #endregion
    }
}
