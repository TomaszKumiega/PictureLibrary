using PictureLibraryModel.DataProviders.Repositories;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryViewModel.Attributes;
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
        private readonly ILibraryRepository _libraryRepository;
        private readonly IImplementationSelector<DataSourceType, ILibraryBuilder> _libraryBuilderImplementationSelector;

        private string LocalStorageString => "Ten komputer";
        #endregion

        #region Public properties
        public bool IsValid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Directory { get; set; }

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
                _settingsProvider.Settings.RemoteStorageInfos.ToList().ForEach(x => list.Add(x.Name));

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

        #region Commands
        [Command]
        public ICommand AddLibraryCommand { get; set; }
        #endregion

        #region Events
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public AddLibraryDialogViewModel(
            ILibraryExplorerViewModel commonVM, 
            ILibraryRepository libraryRepository,
            ICommandCreator commandCreator,
            ISettingsProvider settingsProviderService, 
            IImplementationSelector<DataSourceType, ILibraryBuilder>libraryBuilderImplementationSelector)
        {
            _commonViewModel = commonVM;
            _libraryRepository = libraryRepository;
            _settingsProvider = settingsProviderService;
            _libraryBuilderImplementationSelector = libraryBuilderImplementationSelector;

            commandCreator.InitializeCommands(this);
        }

        #region Command methods
        [Execute(nameof(AddLibraryCommand))]
        private async void ExecuteAddLibraryCommand(object parameter)
        {
            await AddAsync();
        }
        #endregion

        #region Private methods
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Library CreateLibrary()
        {
            var selectedStorageInfo = GetSelectedStorage();
            DataSourceType storageType = selectedStorageInfo != null
                ? selectedStorageInfo.DataSourceType
                : DataSourceType.Local;

            var libraryBuilder = _libraryBuilderImplementationSelector.Select(storageType);

            if (libraryBuilder is IRemoteLibraryBuilder remoteLibraryBuilder)
            {
                return remoteLibraryBuilder.CreateLibrary()
                        .WithName(Name)
                        .WithDescription(Description)
                        .WithRemoteStorageInfo(selectedStorageInfo.Id)
                        .Build();
            }
            else if (libraryBuilder is ILocalLibraryBuilder localLibraryBuilder)
            {
                return localLibraryBuilder.CreateLibrary()
                    .WithName(Name)
                    .WithDescription(Description)
                    .WithPath(Directory + "\\" + Name + "\\" + Name + ".plib")
                    .Build();
            }

            return null;
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
            bool doesntRequireDirectory = SelectedStorage != LocalStorageString;
            bool directoryIsValid = SelectedStorage == LocalStorageString
                    && !string.IsNullOrEmpty(Directory)
                    && System.IO.Directory.Exists(Directory);

            if (doesntRequireDirectory || directoryIsValid) 
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

            return _settingsProvider.Settings.RemoteStorageInfos.Single(x => x.Name == SelectedStorage);
        }
        #endregion
        #endregion

        #region Public methods
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
            
            Library library = CreateLibrary();

            try
            {
                await Task.Run(() => _libraryRepository.AddLibrary(library));
            }
            catch (Exception)
            {
                ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Failed));
            }

            _commonViewModel.RefreshView(this, EventArgs.Empty);

            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
        #endregion
    }
}
