using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Model.UserModel;
using PictureLibraryModel.Repositories.DatabaseRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesProvider
{
    public interface IConnectedServicesProviderService
    {
        IConnectedServiceRepository ConnectedServiceRepository { get; }
        List<ConnectedService> ConnectedServices { get; set; }
        User User { get; }

        Task Update();
    }
}