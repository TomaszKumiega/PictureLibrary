using PictureLibraryModel.Repositories.LibraryRepositories;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerViewModelFactory : ILibraryExplorerViewModelFactory
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private IConnectedServicesInfoProviderService ConnectedServices { get; }
        private ILibraryRepositoryStrategyFactory LibraryRepositoriesFactory { get; }

        public LibraryExplorerViewModelFactory(ILibraryExplorerViewModel commonVM, IConnectedServicesInfoProviderService connectedServices, ILibraryRepositoryStrategyFactory libraryRepositoriesFactory)
        {
            CommonViewModel = commonVM;
            ConnectedServices = connectedServices;
            LibraryRepositoriesFactory = libraryRepositoriesFactory;
        }

        public ILibraryExplorerToolboxViewModel GetLibraryExplorerToolboxViewModel(IClipboardService clipboard)
        {
            return new LibraryExplorerToolboxViewModel(CommonViewModel, new CommandFactory(), clipboard);
        }

        public async Task<IExplorableElementsTreeViewModel> GetLibraryTreeViewModelAsync()
        {
            var viewModel = new LibraryTreeViewModel(new LibraryRepository(ConnectedServices, LibraryRepositoriesFactory), CommonViewModel);
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
