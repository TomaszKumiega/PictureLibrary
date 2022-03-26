using PictureLibraryViewModel.ViewModel.Events;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddLibraryDialogViewModel : INotifyPropertyChanged
    {
        string Name { get; set; }
        string Description { get; set; }
        string Directory { get; set; }
        string SelectedStorage { get; set; }
        List<string> Storages { get; }
        ICommand AddLibraryCommand { get; }

        event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        Task AddAsync();
    }
}
