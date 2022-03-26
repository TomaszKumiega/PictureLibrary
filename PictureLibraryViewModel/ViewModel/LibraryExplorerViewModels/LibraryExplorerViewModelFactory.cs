using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryViewModel.Commands;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    //TODO: remove
    public class LibraryExplorerViewModelFactory : ILibraryExplorerViewModelFactory
    {
        private readonly ILibraryExplorerViewModel _commonViewModel;
        private readonly IDataSourceCollection _dataSourceCollection;
        private readonly ICommandFactory _commandFactory;
        private readonly ISettingsProvider _settingsProvider;

        public LibraryExplorerViewModelFactory(
            ILibraryExplorerViewModel commonVM, 
            IDataSourceCollection dataSourceCollection, 
            ICommandFactory commandFactory, 
            ISettingsProvider settingsProvider)
        {
            _commonViewModel = commonVM;
            _commandFactory = commandFactory;
            _dataSourceCollection = dataSourceCollection;
            _settingsProvider = settingsProvider;
        }

        public ILibraryExplorerToolboxViewModel GetLibraryExplorerToolboxViewModel(IClipboardService clipboard)
        {
            return new LibraryExplorerToolboxViewModel(_commonViewModel, _commandFactory, clipboard);
        }

        public async Task<IExplorableElementsTreeViewModel> GetLibraryTreeViewModelAsync()
        {
            var viewModel = new LibraryTreeViewModel(_dataSourceCollection, _commonViewModel, _settingsProvider);
            await viewModel.InitializeLibraryTreeAsync();
            return viewModel;
        }

        public async Task<IExplorableElementsViewViewModel> GetLibraryViewViewModelAsync()
        {
            var viewModel = new LibraryViewViewModel(_commonViewModel);
            await viewModel.CommonViewModel.LoadCurrentlyShownElementsAsync();
            return viewModel;
        }

        public ITagPanelViewModel GetTagPanelViewModel()
        {
            return new TagPanelViewModel(_commonViewModel);
        }
    }
}
