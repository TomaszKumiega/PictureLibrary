using PictureLibraryModel.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IDialogViewModelFactory
    {
        IAddTagDialogViewModel GetAddTagDialogViewModel();
        Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages);
    }
}
