using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryWPF.Dialogs
{
    public class DialogFactory : IDialogFactory
    {
        private IDialogViewModelFactory _viewModelFactory;

        public DialogFactory(IDialogViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public AddImagesDialog GetAddImagesDialog(List<ImageFile> selectedImages)
        {
            return new AddImagesDialog(_viewModelFactory.GetImagesDialogViewModel(selectedImages));
        }

        public AddLibraryDialog GetAddLibraryDialog()
        {
            return new AddLibraryDialog(_viewModelFactory.GetAddLibraryDialogViewModel());
        }

        public AddTagDialog GetAddTagDialog()
        {
            return new AddTagDialog(_viewModelFactory.GetAddTagDialogViewModel());
        }
    }
}
