using PictureLibraryModel.Model;
using PictureLibraryModel.Model.ConnectedServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesInfoProvider
{
    public interface IConnectedServicesInfoProviderService
    {
        ConnectedServiceInfo GoogleDriveInfo { get; set; }
        ConnectedServiceInfo RemoteServerInfo { get; set; }

        List<Origin> GetAllAvailableOrigins();
        void LoadServicesInfo();
        Task SaveServicesInfoAsync();
    }
}