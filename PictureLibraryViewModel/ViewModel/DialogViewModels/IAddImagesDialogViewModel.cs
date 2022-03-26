using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddImagesDialogViewModel
    {
        IEnumerable<ImageFile> SelectedImages { set; }
        List<Library> Libraries { get; }
        Library SelectedLibrary { get; set; }
        ICommand AddImagesCommand { get; }

        event ProcessingStatusChangedEventHandler ProcessingStatusChanged;

        Task AddAsync();
    }
}
