using PictureLibraryModel.Model.ConnectedServices;

namespace PictureLibraryModel.Services.ConnectedServicesInfoProvider
{
    public interface IConnectedServicesInfoProviderService
    {
        ConnectedServiceInfo GoogleDriveInfo { get; set; }
        ConnectedServiceInfo RemoteServerInfo { get; set; }

        void LoadServicesInfo();
        void SaveServicesInfo();
    }
}