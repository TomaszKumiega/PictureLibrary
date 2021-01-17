using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
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
        List<IExplorableElement> SelectedElements { get; set; }
        IExplorableElement SelectedNode { get; set; }
        ICommand CopyFileCommand { get; }
        ICommand PasteCommand { get; }
        ICommand CutCommand { get; }
        ICommand RenameCommand { get; }
        IClipboardService Clipboard { get; }
        ObservableCollection<IExplorableElement> ExplorableElementsTree { get; }
        ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }

        void CopySelectedElements();
        void CutSelectedElements();
        void PasteSelectedElements();
        void RemoveSelectedElements();
        void RenameSelectedElements();
    }
}
