using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model.Builders;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.SettingsProvider;
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
    public class AddLibraryDialogViewModel : IAddLibraryDialogViewModel
    {
        #region Private fields
        private readonly ILibraryExplorerViewModel _commonViewModel;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IImplementationSelector<int, ILibraryBuilder> _libraryBuilderImplementationSelector;
        private readonly IDataSourceCollection _dataSourceCollection;

        private string LocalStorageString => "Ten komputer";
        #endregion

        #region Public properties
        public bool IsValid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Directory { get; set; }
        public ICommand AddLibraryCommand { get; }

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
                _settingsProvider.Settings.RemoteStorageInfos.ForEach(x => list.Add(x.Name));

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
                NotifyPropertyChanged(nameof(IsPathSelectionEnabled));
            }
        }
        #endregion

        #region Events
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public AddLibraryDialogViewModel(IDataSourceCollection dataSourceCollection, ILibraryExplorerViewModel commonVM, ICommandFactory commandFactory, ISettingsProvider settingsProviderService, IImplementationSelector<int, ILibraryBuilder> libraryBuilderImplementationSelector)
        {
            _dataSourceCollection = dataSourceCollection;
            _commonViewModel = commonVM;
            AddLibraryCommand = commandFactory.GetAddLibraryCommand(this);
            _settingsProvider = settingsProviderService;
            _libraryBuilderImplementationSelector = libraryBuilderImplementationSelector;

            _dataSourceCollection.Initialize(settingsProviderService.Settings.RemoteStorageInfos);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Validation
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
            if (SelectedStorage != null && (SelectedStorage == LocalStorageString || _settingsProvider.Settings.RemoteStorageInfos.Any(x => x.Name == SelectedStorage)))
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

            return _settingsProvider.Settings.RemoteStorageInfos.FirstOrDefault(x => x.Name == SelectedStorage);
        }
        #endregion

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

            var libraryBuilder = _libraryBuilderImplementationSelector.Select(storageType);
            var library = libraryBuilder.CreateLibrary()
                .WithName(Name)
                .WithDescription(Description)
                .WithStorageInfoId(selectedStorage?.Id)
                .Build();

            if (storageType == -1)
            {
                library.Path = Directory + "\\" + Name + "\\" + Name + ".plib";
            }

            var dataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(selectedStorage?.Id);

            try
            {
                await Task.Run(() => dataSource.LibraryProvider.AddLibrary(library));
            }
            catch (Exception)
            {
                ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Failed));
            }

            _commonViewModel.RefreshView(this, EventArgs.Empty);

            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
