using PictureLibraryModel.Repositories.LibraryRepositories;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
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
        public LibraryExplorerViewModelFactory(ILibraryExplorerViewModel commonVM, IConnectedServicesInfoProviderService connectedServices)
        {
            CommonViewModel = commonVM;
            ConnectedServices = connectedServices;
        }

        public ILibraryExplorerToolboxViewModel GetLibraryExplorerToolboxViewModel(IClipboardService clipboard)
        {
            return new LibraryExplorerToolboxViewModel(CommonViewModel, new CommandFactory(), clipboard);
        }

        public async Task<IExplorableElementsTreeViewModel> GetLibraryTreeViewModel()
        {
            var viewModel = new LibraryTreeViewModel(new LibraryRepository(ConnectedServices, new LibraryRepositoriesFactory()), CommonViewModel);
            await viewModel.InitializeLibraryTree();
            return viewModel;
        }

        public IExplorableElementsViewViewModel GetLibraryViewViewModel()
        {
            return new LibraryViewViewModel(CommonViewModel);
        }

        public ITagPanelViewModel GetTagPanelViewModel()
        {
            return new TagPanelViewModel(CommonViewModel);
        }
    }
}
