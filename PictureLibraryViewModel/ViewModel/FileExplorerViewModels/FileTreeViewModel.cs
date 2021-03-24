using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileTreeViewModel : IExplorableElementsTreeViewModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IExplorableElement _selectedNode;

        private IDirectoryService DirectoryService { get; }

        public IExplorerViewModel CommonViewModel { get; }

        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }
        public IExplorableElement SelectedNode 
        { 
            get => _selectedNode; 
            set
            {
                _selectedNode = value;
                CommonViewModel.CurrentlyOpenedElement = _selectedNode;
            }
        }

        public FileTreeViewModel(IFileExplorerViewModel viewModel, IDirectoryService directoryService)
        {
            CommonViewModel = viewModel;
            DirectoryService = directoryService;
        }

        public async Task InitializeDirectoryTreeAsync()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            IEnumerable<Directory> rootDirectories = new List<Directory>();

            try
            {
                rootDirectories = await Task.Run(() => DirectoryService.GetRootDirectories());
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the directory tree");
            }

            foreach (var t in rootDirectories)
            {
                await t.LoadSubDirectoriesAsync();
                ExplorableElementsTree.Add(t);
            }

            CommonViewModel.CurrentlyOpenedElement = null;
        }
    }
}
