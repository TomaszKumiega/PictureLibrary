using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Model.UserModel;
using PictureLibraryModel.Repositories.DatabaseRepositories;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesProvider
{
    public interface IConnectedServicesProviderService : INotifyPropertyChanged
    {
        IConnectedServiceRepository ConnectedServiceRepository { get; }
        List<ConnectedService> ConnectedServices { get; set; }
        User User { get; }

        Task Update();
    }
}