using PictureLibraryModel.Model;
using System.Collections.ObjectModel;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorableElementsTreeViewModel
    {
        IExplorerViewModel CommonViewModel { get; }
        IExplorableElement SelectedNode { get; set; }
        ObservableCollection<IExplorableElement> ExplorableElementsTree { get; }
    }
}
