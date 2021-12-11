using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerViewModelFactory : ILibraryExplorerViewModelFactory
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private IDataSourceCollection DataSourceCollection { get; }
        private ICommandFactory CommandFactory { get; }

        public LibraryExplorerViewModelFactory(ILibraryExplorerViewModel commonVM, IDataSourceCollection dataSourceCollection, ICommandFactory commandFactory)
        {
            CommonViewModel = commonVM;
            CommandFactory = commandFactory;
            DataSourceCollection = dataSourceCollection;
        }

        public ILibraryExplorerToolboxViewModel GetLibraryExplorerToolboxViewModel(IClipboardService clipboard)
        {
            return new LibraryExplorerToolboxViewModel(CommonViewModel, CommandFactory, clipboard);
        }

        public async Task<IExplorableElementsTreeViewModel> GetLibraryTreeViewModelAsync()
        {
            var viewModel = new LibraryTreeViewModel(DataSourceCollection, CommonViewModel);
            await viewModel.InitializeLibraryTreeAsync();
            return viewModel;
        }

        public async Task<IExplorableElementsViewViewModel> GetLibraryViewViewModelAsync()
        {
            var viewModel = new LibraryViewViewModel(CommonViewModel);
            await viewModel.CommonViewModel.LoadCurrentlyShownElementsAsync();
            return viewModel;
        }

        public ITagPanelViewModel GetTagPanelViewModel()
        {
            return new TagPanelViewModel(CommonViewModel);
        }
    }
}
