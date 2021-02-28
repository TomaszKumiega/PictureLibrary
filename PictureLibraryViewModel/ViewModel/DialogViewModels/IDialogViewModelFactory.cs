using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IDialogViewModelFactory
    {
        IAddTagDialogViewModel GetAddTagDialogViewModel();
        IAddLibraryDialogViewModel GetAddLibraryDialogViewModel();
        IAddImagesDialogViewModel GetImagesDialogViewModel(List<ImageFile> selectedImages);
    }
}
