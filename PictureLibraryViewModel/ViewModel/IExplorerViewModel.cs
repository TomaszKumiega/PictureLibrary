﻿using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;

        string CurrentlyOpenedPath { get; set; }
        ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        ObservableCollection<IExplorableElement> SelectedElements { get; set; }
    }
}
