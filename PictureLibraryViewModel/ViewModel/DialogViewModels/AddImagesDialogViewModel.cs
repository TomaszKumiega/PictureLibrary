﻿using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System.Collections.Generic;
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedLibrary)));
            }
        }
        public IEnumerable<ImageFile> SelectedImages { private get; set; }
        #endregion

        #region Commands
        [Command]
        public ICommand AddImagesCommand { get; set; }
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
        #endregion

        #region Public methods
        public async Task AddAsync()
        {
            foreach(var t in SelectedImages)
            {
                var dataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(t.RemoteStorageInfoId);
                await Task.Run(() => dataSource.ImageProvider.AddImageToLibrary(t, SelectedLibrary.Path));
                SelectedLibrary.Images.Add(t);
                dataSource.LibraryProvider.UpdateLibrary(SelectedLibrary);
            }

            SelectedLibrary.Images.AddRange(SelectedImages);

            var libraryDataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(SelectedLibrary.RemoteStorageInfoId);
            await Task.Run(() => libraryDataSource.LibraryProvider.UpdateLibrary(SelectedLibrary));

            await _commonViewModel.LoadCurrentlyShownElementsAsync();

            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
        #endregion
    }
}
