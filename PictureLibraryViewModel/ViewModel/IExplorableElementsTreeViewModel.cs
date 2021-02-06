using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorableElementsTreeViewModel
    {
        IExplorerViewModel CommonViewModel { get; }
        IExplorableElement SelectedNode { get; set; }
        ObservableCollection<IExplorableElement> ExplorableElementsTree { get; }
    }
}
