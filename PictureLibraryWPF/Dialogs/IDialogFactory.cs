using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryWPF.Dialogs
{
    //TODO: remove
    public interface IDialogFactory
    {
        Task<AddImagesDialog> GetAddImagesDialog(List<ImageFile> selectedFiles);
        AddTagDialog GetAddTagDialog();
    }
}
