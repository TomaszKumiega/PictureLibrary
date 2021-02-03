using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorerViewModel : INotifyPropertyChanged
    {
        IExplorerHistory ExplorerHistory { get; }
        ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        IExplorableElement SelectedNode { get; set; }
        ICommand CopyCommand { get; }
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        ICommand RenameCommand { get; }
        ICommand BackCommand { get; }
        ICommand ForwardCommand { get; }
        IClipboardService Clipboard { get; }
        ObservableCollection<IExplorableElement> ExplorableElementsTree { get; }
        ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }

        void CopySelectedElements();
        void CutSelectedElements();
        void PasteSelectedElements();
        void RemoveSelectedElements();
        void RenameSelectedElements();
        void Back();
        void Forward();
    }
}
