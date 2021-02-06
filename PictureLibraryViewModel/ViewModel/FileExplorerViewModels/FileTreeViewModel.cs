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
    public class FileTreeViewModel : IFileTreeViewModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IExplorableElement _selectedNode;

        public IExplorerViewModel CommonViewModel { get; }
        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }
        public IExplorableElement SelectedNode 
        { 
            get => _selectedNode; 
            set
            {
                _selectedNode = value;
                (CommonViewModel as IFileExplorerViewModel).CurrentlyOpenedPath = _selectedNode.FullPath;
            }
        }

        public FileTreeViewModel(IFileExplorerViewModel viewModel)
        {
            CommonViewModel = viewModel;
            InitializeDirectoryTree();
        }

        private void InitializeDirectoryTree()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            IEnumerable<Directory> rootDirectories = new List<Directory>();

            try
            {
                rootDirectories = CommonViewModel.DirectoryService.GetRootDirectories();
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the directory tree");
            }

            foreach (var t in rootDirectories)
            {
                Task.Run(() => t.LoadSubDirectoriesAsync()).Wait();
                ExplorableElementsTree.Add(t);
            }

            CommonViewModel.CurrentlyOpenedPath = "\\";
        }
    }
}
