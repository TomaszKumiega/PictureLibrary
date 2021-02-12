using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Model.UserModel;
using PictureLibraryModel.Repositories.DatabaseRepositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ConnectedServicesProvider
{
    public interface IConnectedServicesProviderService : INotifyPropertyChanged
    {
        ConnectedService RemoteServer { get; set; }
        ConnectedService GoogleDrive { get; set; }
        User User { get; }

        Task LoadServices();
        Task SaveChanges();
    }
}