using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddImagesDialogViewModel : IAddImagesDialogViewModel, INotifyPropertyChanged
    {
        private readonly ILibraryExplorerViewModel _commonViewModel;
        private readonly List<ImageFile> _selectedImages;
        private readonly IDataSourceCollection _dataSourceCollection;

        public event PropertyChangedEventHandler PropertyChanged;
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;


        public ICommand AddImagesCommand { get; }
        public List<Library> Libraries { get; private set; }

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

        public AddImagesDialogViewModel(
            ILibraryExplorerViewModel commonVM, 
            IDataSourceCollection dataSourceCollection, 
            List<ImageFile> selectedImages, 
            ICommandFactory commandFactory)
        {
            _commonViewModel = commonVM;
            _selectedImages = selectedImages;
            AddImagesCommand = commandFactory.GetAddImagesCommand(this);
            _dataSourceCollection = dataSourceCollection;

            _dataSourceCollection.Initialize(new List<IRemoteStorageInfo>());

            PropertyChanged += OnSelectedLibraryChanged;
        }

        private void OnSelectedLibraryChanged(object sender, PropertyChangedEventArgs args)
        {
            (AddImagesCommand as AddImagesCommand).RaiseCanExecuteChanged(this, EventArgs.Empty);
        }

        public async Task Initialize()
        {
            Libraries = await Task.Run(() => _dataSourceCollection.GetAllLibraries());
        }

        public async Task AddAsync()
        {
            foreach(var t in _selectedImages)
            {
                var dataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(t.RemoteStorageInfoId);
                await Task.Run(() => dataSource.ImageProvider.AddImageToLibrary(t, SelectedLibrary.Path));
                SelectedLibrary.Images.Add(t);
                dataSource.LibraryProvider.UpdateLibrary(SelectedLibrary);
            }

            SelectedLibrary.Images.AddRange(_selectedImages);

            var libraryDataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(SelectedLibrary.RemoteStorageInfoId);
            await Task.Run(() => libraryDataSource.LibraryProvider.UpdateLibrary(SelectedLibrary));

            await _commonViewModel.LoadCurrentlyShownElementsAsync();

            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
