using PictureLibraryModel.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel
{
    public interface IExplorableElementsTreeViewModel
    {
        IExplorableElement SelectedNode { get; set; }
        ObservableCollection<IExplorableElement> ExplorableElementsTree { get; }

        Task Initialize();
    }
}
