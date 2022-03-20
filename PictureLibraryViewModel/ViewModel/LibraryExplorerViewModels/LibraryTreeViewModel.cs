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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IExplorableElement _selectedNode;
        private IDataSourceCollection DataSourceCollection { get; set; }

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

        public LibraryTreeViewModel(IDataSourceCollection dataSourceCollection, IExplorerViewModel commonVM, ISettingsProvider settingsProvider)
        {
            DataSourceCollection = dataSourceCollection;
            CommonViewModel = commonVM;

            DataSourceCollection.Initialize(settingsProvider.Settings.RemoteStorageInfos);
            ((ILibraryExplorerViewModel)CommonViewModel).RefreshViewEvent += OnRefreshView;
        }

        private async void OnRefreshView(object sender, EventArgs e)
        {
            await InitializeLibraryTreeAsync();
        }

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
                libraries = await Task.Run(() => DataSourceCollection.GetAllLibraries());
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

            CommonViewModel.CurrentlyOpenedElement = null;
        }
    }
}
