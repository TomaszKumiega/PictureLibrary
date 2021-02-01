using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel
{
    public class LibraryExplorerViewModel : ILibraryExplorerViewModel
    { 
        public ObservableCollection<IExplorableElement> SelectedElements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IExplorableElement SelectedNode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand CopyCommand => throw new NotImplementedException();

        public ICommand PasteCommand => throw new NotImplementedException();

        public ICommand CutCommand => throw new NotImplementedException();

        public ICommand RenameCommand => throw new NotImplementedException();

        public IClipboardService Clipboard => throw new NotImplementedException();

        public ObservableCollection<IExplorableElement> ExplorableElementsTree => throw new NotImplementedException();

        public ObservableCollection<IExplorableElement> CurrentlyShownElements => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopySelectedElements()
        {
            throw new NotImplementedException();
        }

        public void CutSelectedElements()
        {
            throw new NotImplementedException();
        }

        public void PasteSelectedElements()
        {
            throw new NotImplementedException();
        }

        public void RemoveSelectedElements()
        {
            throw new NotImplementedException();
        }

        public void RenameSelectedElements()
        {
            throw new NotImplementedException();
        }
    }
}
