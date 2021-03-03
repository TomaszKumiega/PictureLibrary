using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryWPF.Dialogs
{
    public class DialogFactory : IDialogFactory
    {
        private IDialogViewModelFactory _viewModelFactory;

        public DialogFactory(IDialogViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public async Task<AddImagesDialog> GetAddImagesDialog(List<ImageFile> selectedImages)
        {
            return new AddImagesDialog(await _viewModelFactory.GetImagesDialogViewModel(selectedImages));
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
