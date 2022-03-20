using PictureLibraryViewModel.ViewModel.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddTagDialogViewModel
    {
        string Name { get; set; }
        string Color { get; set; }
        string Description { get; set; }
        ICommand AddTagCommand { get; }

        event InvalidInputEventHandler InvalidInput;
        event ProcessingStatusChangedEventHandler ProcessingStatusChanged;

        Task AddAsync();
    }
}
