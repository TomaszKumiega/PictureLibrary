using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddLibraryDialogViewModel : IAddLibraryDialogViewModel
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private ISettingsProvider SettingsProvider { get; }
        private IImplementationSelector<int, ILibraryBuilder> LibraryBuilderImplementationSelector { get; }
        private IDataSourceCollection DataSourceCollection { get; }

        private string LocalStorageString => "Ten komputer";

        public bool IsValid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Directory { get; set; }
        public ICommand AddLibraryCommand { get; }

        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public AddLibraryDialogViewModel(IDataSourceCollection dataSourceCollection, ILibraryExplorerViewModel commonVM, ICommandFactory commandFactory, ISettingsProvider settingsProviderService, IImplementationSelector<int, ILibraryBuilder> libraryBuilderImplementationSelector)
        {
            DataSourceCollection = dataSourceCollection;
            CommonViewModel = commonVM;
            AddLibraryCommand = commandFactory.GetAddLibraryCommand(this);
            SettingsProvider = settingsProviderService;
            LibraryBuilderImplementationSelector = libraryBuilderImplementationSelector;

            DataSourceCollection.Initialize(settingsProviderService.Settings.RemoteStorageInfos);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _selectedStorage;
        public string SelectedStorage 
        { 
            get => _selectedStorage; 
            set
            {
                IsPathSelectionEnabled = value == LocalStorageString; 
                _selectedStorage = value;
            }
        }

        public List<string> Storages 
        { 
            get
            {
                var list = new List<string>();

                list.Add(LocalStorageString);
                SettingsProvider.Settings.RemoteStorageInfos.ForEach(x => list.Add(x.Name));

                return list;
            }
        }

        private bool _isPathSelectionEnabled;
        public bool IsPathSelectionEnabled 
        { 
            get => _isPathSelectionEnabled; 
            private set
            {
                _isPathSelectionEnabled = value;
                OnPropertyChanged(nameof(IsPathSelectionEnabled));
            }
        }

        private bool IsNameValid()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return true;
        }

        private bool IsSelectedStorageValid()
        {
            if (SelectedStorage != null && (SelectedStorage == LocalStorageString || SettingsProvider.Settings.RemoteStorageInfos.Any(x => x.Name == SelectedStorage)))
            {
                return true;
            }

            return false;
        }

        private bool IsDirectoryValid()
        {
            if (SelectedStorage == LocalStorageString && !string.IsNullOrEmpty(Directory) && System.IO.Directory.Exists(Directory))
            {
                return true;
            }

            return false;
        }

        private bool IsObjectValid()
        {
            if (!IsNameValid()) return false;
            if (!IsSelectedStorageValid()) return false;  
            if (!IsDirectoryValid()) return false;

            return true;
        }

        private IRemoteStorageInfo GetSelectedStorage()
        {
            if (SelectedStorage == LocalStorageString)
                return null;

            return SettingsProvider.Settings.RemoteStorageInfos.FirstOrDefault(x => x.Name == SelectedStorage);
        }

        public async Task AddAsync()
        {
            if (!IsObjectValid())
            {
                IsValid = false;
                return;
            }
            else
            {
                IsValid = true;
            }

            var selectedStorage = GetSelectedStorage();
            var storageType = selectedStorage == null ? -1 : (int)selectedStorage.StorageType;

            var libraryBuilder = LibraryBuilderImplementationSelector.Select(storageType);
            var library = libraryBuilder.CreateLibrary()
                .WithName(Name)
                .WithDescription(Description)
                .WithStorageInfoId(selectedStorage?.Id)
                .Build();

            if (storageType == -1)
            {
                library.Path = Directory + Path.DirectorySeparatorChar + Name + ".plib";
            }

            var dataSource = DataSourceCollection.GetDataSourceByRemoteStorageId(selectedStorage?.Id);

            try
            {
                await Task.Run(() => dataSource.LibraryProvider.AddLibrary(library));
            }
            catch (Exception)
            {
                ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Failed));
            }

            CommonViewModel.RefreshView(this, EventArgs.Empty);

            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
