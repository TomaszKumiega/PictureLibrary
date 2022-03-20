using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddImagesDialogViewModel : IAddImagesDialogViewModel, INotifyPropertyChanged
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private List<ImageFile> SelectedImages { get; }
        private IDataSourceCollection DataSourceCollection { get; }
        private Library _selectedLibrary;


        public event PropertyChangedEventHandler PropertyChanged;
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;

        public ICommand AddImagesCommand { get; }
        public List<Library> Libraries { get; private set; }
        public Library SelectedLibrary 
        {
            get => _selectedLibrary; 
            set
            {
                _selectedLibrary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedLibrary"));
            }
        }

        public AddImagesDialogViewModel(ILibraryExplorerViewModel commonVM, IDataSourceCollection dataSourceCollection, List<ImageFile> selectedImages, ICommandFactory commandFactory)
        {
            CommonViewModel = commonVM;
            SelectedImages = selectedImages;
            AddImagesCommand = commandFactory.GetAddImagesCommand(this);
            DataSourceCollection = dataSourceCollection;

            DataSourceCollection.Initialize(new List<IRemoteStorageInfo>());

            PropertyChanged += OnSelectedLibraryChanged;
        }

        private void OnSelectedLibraryChanged(object sender, PropertyChangedEventArgs args)
        {
            (AddImagesCommand as AddImagesCommand).RaiseCanExecuteChanged(this, EventArgs.Empty);
        }

        public async Task Initialize()
        {
            Libraries = await Task.Run(() => DataSourceCollection.GetAllLibraries());
        }

        public async Task AddAsync()
        {
            foreach(var t in SelectedImages)
            {
                var dataSource = DataSourceCollection.GetDataSourceByRemoteStorageId(t.RemoteStorageInfoId);
                await Task.Run(() => dataSource.ImageProvider.AddImageToLibrary(t, SelectedLibrary.Path));
            }

            SelectedLibrary.Images.AddRange(SelectedImages);

            var libraryDataSource = DataSourceCollection.GetDataSourceByRemoteStorageId(SelectedLibrary.RemoteStorageInfoId);
            await Task.Run(() => libraryDataSource.LibraryProvider.UpdateLibrary(SelectedLibrary));

            await CommonViewModel.LoadCurrentlyShownElementsAsync();

            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
