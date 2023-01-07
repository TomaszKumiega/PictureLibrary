using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DataProviders.Repositories;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddImagesDialogViewModel : IAddImagesDialogViewModel, INotifyPropertyChanged
    {
        #region Private fields
        private readonly ILibraryRepository _libraryRepository;
        private readonly ILibraryExplorerViewModel _commonViewModel;
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
            ICommandCreator commandCreator,
            ILibraryExplorerViewModel commonVM,
            ILibraryRepository libraryRepository)
        {
            _commonViewModel = commonVM;
            _libraryRepository = libraryRepository;

            Libraries = libraryRepository.Query().GetAll().ToList();
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
            foreach (var t in SelectedImages)
            {
                t.Tags = SelectedTags.ToList();
                var savedImageFile = await Task.Run(() => dataSource.ImageProvider.AddImageToLibrary(t, SelectedLibrary));
                SelectedLibrary.Images.Add(savedImageFile);
            }

            await Task.Run(() => _libraryRepository.UpdateLibrary(SelectedLibrary));

            await _commonViewModel.LoadCurrentlyShownElementsAsync();
            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
        #endregion
    }
}
