using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileTreeViewModel : IFileTreeViewModel
    {
        #region Private fields
        private readonly IDirectoryService _directoryService;
        private readonly IFileExplorerViewModel _commonViewModel;
        #endregion

        #region Public properties
        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }

        private IExplorableElement _selectedNode;
        public IExplorableElement SelectedNode 
        { 
            get => _selectedNode; 
            set
            {
                _selectedNode = value;
                _commonViewModel.CurrentlyOpenedElement = _selectedNode;
            }
        }
        #endregion

        public FileTreeViewModel(IFileExplorerViewModel viewModel, IDirectoryService directoryService)
        {
            _commonViewModel = viewModel;
            _directoryService = directoryService;
        }

        public async Task Initialize()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            var rootDirectories = await Task.Run(() => _directoryService.GetRootDirectories());

            foreach (var t in rootDirectories)
            {
                await t.LoadSubDirectoriesAsync();
                ExplorableElementsTree.Add(t);
                t.LoadIcon();
            }
        }
    }
}
