using NLog;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryTreeViewModel : IExplorableElementsTreeViewModel
    {
        #region Private fields
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDataSourceCollection _dataSourceCollection;
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

        public LibraryTreeViewModel(IDataSourceCollection dataSourceCollection, IExplorerViewModel commonVM, ISettingsProvider settingsProvider)
        {
            _dataSourceCollection = dataSourceCollection;
            CommonViewModel = commonVM;

            _dataSourceCollection.Initialize(settingsProvider.Settings.RemoteStorageInfos);
            ((ILibraryExplorerViewModel)CommonViewModel).RefreshViewEvent += OnRefreshView;
        }

        #region Event handlers
        private async void OnRefreshView(object sender, EventArgs e)
        {
            await InitializeLibraryTreeAsync();
        }
        #endregion

        public async Task InitializeLibraryTreeAsync()
        {
            if (ExplorableElementsTree == null)
            {
                ExplorableElementsTree = new ObservableCollection<IExplorableElement>();
            }
            else
            {
                ExplorableElementsTree.Clear();
            }
            
            IEnumerable<Library> libraries = new List<Library>();

            try
            {
                libraries = await Task.Run(() => _dataSourceCollection.GetAllLibraries());
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the library tree.");
            }

            foreach(var t in libraries)
            {
                ExplorableElementsTree.Add(t);
                t.LoadIcon();
            }
        }
    }
}
