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
        private IRepository<Library> LibraryRepository { get; }

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
            LibraryRepository = libraryRepository;
            CommonViewModel = commonVM;
        }

        public async Task InitializeLibraryTree()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();
            IEnumerable<Library> libraries = new List<Library>();

            try
            {
                libraries = await LibraryRepository.GetAllAsync();
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
