﻿using PictureLibraryModel.Model;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public interface IFileExplorerViewModel : IExplorerViewModel
    {
        IExplorerHistory ExplorerHistory { get; }

        void Back();
        void Forward();
    }
}
