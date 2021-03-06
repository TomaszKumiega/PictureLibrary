﻿using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerToolboxViewModel
    { 
        IExplorerViewModel CommonViewModel { get; }
        IClipboardService Clipboard { get; }
        ICommand RemoveCommand { get; }
        ICommand RenameCommand { get; }
        ICommand RefreshCommand { get; }
        string SearchText { get; set; }

        Task Remove();
        Task Rename();
        Task Refresh();
        bool IsDriveSelected();
    }
}
