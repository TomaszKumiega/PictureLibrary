using PictureLibraryModel.DataProviders.Repositories;
using PictureLibraryModel.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryTreeViewModel : ILibraryTreeViewModel
    {
        #region Private fields
        private readonly IExplorerViewModel _commonViewModel;
        private readonly ILibraryRepository _libraryRepository;
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

        public LibraryTreeViewModel(
            ILibraryExplorerViewModel commonVM,
            ILibraryRepository libraryRepository)
        {
            _commonViewModel = commonVM;
            _libraryRepository = libraryRepository;

            ((ILibraryExplorerViewModel)_commonViewModel).RefreshViewEvent += OnRefreshView;
        }

        #region Event handlers
        private async void OnRefreshView(object sender, EventArgs e)
        {
            await Initialize();
        }
        #endregion

        public async Task Initialize()
        {
            if (ExplorableElementsTree == null)
            {
                ExplorableElementsTree = new ObservableCollection<IExplorableElement>();
            }
            else
            {
                ExplorableElementsTree.Clear();
            }

            var libraries = await Task.Run(
                () => _libraryRepository.Query()
                                        .GetAll()
                                        .ToList());
                
            foreach (var t in libraries)
            {
                ExplorableElementsTree.Add(t);
                t.LoadIcon();
            }
        }
    }
}
