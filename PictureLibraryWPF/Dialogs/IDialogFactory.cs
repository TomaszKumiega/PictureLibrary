using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryWPF.Dialogs
{
    public interface IDialogFactory
    {
        AddImagesDialog GetAddImagesDialog(List<ImageFile> selectedFiles);
        AddLibraryDialog GetAddLibraryDialog();
        AddTagDialog GetAddTagDialog();
    }
}
