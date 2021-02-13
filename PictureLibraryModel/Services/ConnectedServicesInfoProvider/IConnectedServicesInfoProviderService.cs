using PictureLibraryModel.Model.ConnectedServices;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesInfoProvider
{
    public interface IConnectedServicesInfoProviderService
    {
        ConnectedServiceInfo GoogleDriveInfo { get; set; }
        ConnectedServiceInfo RemoteServerInfo { get; set; }

        void LoadServicesInfo();
        Task SaveServicesInfoAsync();
    }
}