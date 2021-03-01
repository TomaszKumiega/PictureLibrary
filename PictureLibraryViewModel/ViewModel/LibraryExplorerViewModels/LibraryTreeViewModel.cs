using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryModel.Repositories.LibraryRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryTreeViewModel : IExplorableElementsTreeViewModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IExplorableElement _selectedNode;
        IRepository<Library> _libraryRepository;

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

        public LibraryTreeViewModel(IRepository<Library> libraryRepository, IExplorerViewModel commonVM)
        {
            _libraryRepository = libraryRepository;
            CommonViewModel = commonVM;
            InitializeLibraryTree();
        }

        private void InitializeLibraryTree()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();
            IEnumerable<Library> libraries = new List<Library>();

            try
            {
                libraries = Task.Run(() => _libraryRepository.GetAllAsync()).Result;
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the library tree.");
            }

            foreach(var t in libraries)
            {
                ExplorableElementsTree.Add(t);
            }

            CommonViewModel.CurrentlyOpenedElement = null;
        }
    }
}
