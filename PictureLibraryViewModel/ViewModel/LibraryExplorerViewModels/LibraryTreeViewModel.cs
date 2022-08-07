using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryTreeViewModel : ILibraryTreeViewModel
    {
        #region Private fields
        private readonly IDataSourceCollection _dataSourceCollection;
        private IExplorerViewModel _commonViewModel;
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
            IDataSourceCollection dataSourceCollection, 
            ILibraryExplorerViewModel commonVM)
        {
            _dataSourceCollection = dataSourceCollection;
            _commonViewModel = commonVM;

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
            
            var libraries = await Task.Run(() => _dataSourceCollection.GetAllLibraries());
                
            foreach (var t in libraries)
            {
                ExplorableElementsTree.Add(t);
                t.LoadIcon();
            }
        }
    }
}
