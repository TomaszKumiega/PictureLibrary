using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Model.UserModel;
using PictureLibraryModel.Repositories.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesProvider
{
    public class ConnectedServicesProviderService : IConnectedServicesProviderService
    {
        private ConnectedService _remoteServer;
        private ConnectedService _googleDrive;
        private IConnectedServiceRepository ConnectedServiceRepository { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public User User { get; }

        public ConnectedService RemoteServer 
        { 
            get => _remoteServer; 
            set
            {
                _remoteServer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RemoteServer"));
            }
        }

        public ConnectedService GoogleDrive 
        { 
            get => _googleDrive; 
            set
            {
                _googleDrive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GoogleDrive"));
            }
        }

        public ConnectedServicesProviderService(IConnectedServiceRepository connectedServiceRepository, User user)
        {
            ConnectedServiceRepository = connectedServiceRepository;
            User = user;
        }

        public async Task LoadServices()
        {
            var services = await ConnectedServiceRepository.GetByUserId(User.Id);

            RemoteServer = services.FirstOrDefault(x => x.Type == ConnectedServiceType.RemoteServer);
            GoogleDrive = services.FirstOrDefault(x => x.Type == ConnectedServiceType.GoogleDrive);
        }

        public async Task SaveChanges()
        {
            var services = await ConnectedServiceRepository.GetByUserId(User.Id);
            var remoteServer = services.FirstOrDefault(x => x.Type == ConnectedServiceType.RemoteServer);
            var googleDrive = services.FirstOrDefault(x => x.Type == ConnectedServiceType.GoogleDrive);

            if(remoteServer == null && RemoteServer != null)
            {
                await ConnectedServiceRepository.AddAsync(RemoteServer);
            }
            else if(remoteServer != null && RemoteServer == null)
            {
                await ConnectedServiceRepository.RemoveAsync(remoteServer);
            }
            else if(remoteServer != null && RemoteServer != null)
            {
                await ConnectedServiceRepository.UpdateAsync(RemoteServer);
            }

            if(googleDrive == null && GoogleDrive != null)
            {
                await ConnectedServiceRepository.AddAsync(GoogleDrive);
            }
            else if(googleDrive != null && GoogleDrive == null)
            {
                await ConnectedServiceRepository.RemoveAsync(googleDrive);
            }
            else if(googleDrive != null && GoogleDrive != null)
            {
                await ConnectedServiceRepository.UpdateAsync(GoogleDrive);
            }
        }
    }
}
