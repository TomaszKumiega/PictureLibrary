using System.ComponentModel;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IGoogleDriveLoginDialogViewModel : INotifyPropertyChanged
    {
        bool Authorized { get; set; }
        ICommand LoginCommand { get; set; }
        string Username { get; set; }
    }
}