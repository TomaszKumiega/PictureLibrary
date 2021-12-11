using NLog;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
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

        public LibraryTreeViewModel(IDataSourceCollection dataSourceCollection, IExplorerViewModel commonVM)
        {
            DataSourceCollection = dataSourceCollection;
            CommonViewModel = commonVM;
        }

        public async Task InitializeLibraryTreeAsync()
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();
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
            }

            CommonViewModel.CurrentlyOpenedElement = null;
        }
    }
}
