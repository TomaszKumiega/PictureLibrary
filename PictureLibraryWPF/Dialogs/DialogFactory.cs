using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryWPF.Dialogs
{
    //TODO: remove
    public class DialogFactory : IDialogFactory
    {
        private IDialogViewModelFactory ViewModelFactory;

        public DialogFactory(IDialogViewModelFactory viewModelFactory)
        {
            ViewModelFactory = viewModelFactory;
        }

        public async Task<AddImagesDialog> GetAddImagesDialog(List<ImageFile> selectedImages)
        {
            return new AddImagesDialog(await ViewModelFactory.GetImagesDialogViewModel(selectedImages));
        }

        public AddTagDialog GetAddTagDialog()
        {
            return new AddTagDialog(ViewModelFactory.GetAddTagDialogViewModel());
        }
    }
}
