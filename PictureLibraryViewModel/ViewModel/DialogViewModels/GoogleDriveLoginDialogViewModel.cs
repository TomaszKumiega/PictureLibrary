using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class GoogleDriveLoginDialogViewModel : IGoogleDriveLoginDialogViewModel
    {
        private readonly IGoogleDriveApiClient _client;
        private readonly ISettingsProvider _settingsProvider;
        private readonly Func<GoogleDriveRemoteStorageInfo> _googleDriveRemoteStorageInfoLocator;

        public event PropertyChangedEventHandler PropertyChanged;

        [Command]
        public ICommand LoginCommand { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        public bool Authorized { get; set; }

        public GoogleDriveLoginDialogViewModel(
            IGoogleDriveApiClient client,
            ISettingsProvider settingsProvider,
            ICommandCreator commandCreator,
            Func<GoogleDriveRemoteStorageInfo> googleDriveRemoteStorageInfoLocator)
        {
            _client = client;
            _settingsProvider = settingsProvider;
            _googleDriveRemoteStorageInfoLocator = googleDriveRemoteStorageInfoLocator;

            commandCreator.InitializeCommands(this);
        }

        [CanExecute(nameof(LoginCommand))]
        private bool LoginCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Username);
        }

        [Execute(nameof(LoginCommand))]
        private void LoginExecute(object parameter)
        {
            Authorized = _client.Authorize(Username);
            
            var remoteStorageInfo = _googleDriveRemoteStorageInfoLocator();
            remoteStorageInfo.Name = $"Google Drive - {Username}";
            remoteStorageInfo.UserName = Username;
            remoteStorageInfo.Id = Guid.NewGuid();

            _settingsProvider.Settings.RemoteStorageInfos.Add(remoteStorageInfo);
            _settingsProvider.SaveSettings();

            NotifyPropertyChanged(nameof(Authorized));
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
