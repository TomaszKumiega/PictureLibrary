using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Model.UserModel;
using PictureLibraryModel.Repositories.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesProvider
{
    public class ConnectedServicesProviderService : IConnectedServicesProviderService
    {
        public List<ConnectedService> ConnectedServices { get; set; }
        public User User { get; }
        public IConnectedServiceRepository ConnectedServiceRepository { get; }

        public ConnectedServicesProviderService(IConnectedServiceRepository connectedServiceRepository, User user)
        {
            ConnectedServiceRepository = connectedServiceRepository;
            User = user;
            ConnectedServices = new List<ConnectedService>();
        }

        public async Task Update()
        {
            ConnectedServices = (await ConnectedServiceRepository.GetByUserId(User.Id)).ToList();
        }
    }
}
