using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileTreeViewModel : IExplorableElementsTreeViewModel
    {
        #region Private fields
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDirectoryService _directoryService;
        #endregion

        #region Public properties
        public IExplorerViewModel CommonViewModel { get; }
        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; private set; }

        private IExplorableElement _selectedNode;
        public IExplorableElement SelectedNode 
        { 
            get => _selectedNode; 
            set
            {
                _selectedNode = value;
                CommonViewModel.CurrentlyOpenedElement = _selectedNode;
            }
        }
        #endregion

        public FileTreeViewModel(IFileExplorerViewModel viewModel, IDirectoryService directoryService)
        {
            CommonViewModel = viewModel;
            _directoryService = directoryService;
        }

        public async Task InitializeDirectoryTreeAsync()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();

            IEnumerable<Directory> rootDirectories = new List<Directory>();

            try
            {
                rootDirectories = await Task.Run(() => _directoryService.GetRootDirectories());
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
                t.LoadIcon();
            }
        }
    }
}
