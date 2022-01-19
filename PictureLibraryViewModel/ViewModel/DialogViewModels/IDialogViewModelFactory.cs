﻿using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IDialogViewModelFactory
    {
        IAddTagDialogViewModel GetAddTagDialogViewModel();
        Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages);
    }
}
