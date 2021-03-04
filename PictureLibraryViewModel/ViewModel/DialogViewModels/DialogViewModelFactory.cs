using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories.LibraryRepositories;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class DialogViewModelFactory : IDialogViewModelFactory
    {
        private ILibraryExplorerViewModel _commonViewModel;
        private IConnectedServicesInfoProviderService _connectedServices;
        public ILibraryRepositoriesFactory LibraryRepositoriesFactory { get; }

        public DialogViewModelFactory(ILibraryExplorerViewModel commonVM, IConnectedServicesInfoProviderService connectedServices, ILibraryRepositoriesFactory libraryRepositoriesFactory)
        {
            _commonViewModel = commonVM;
            _connectedServices = connectedServices;
            LibraryRepositoriesFactory = libraryRepositoriesFactory;
        }

        public IAddLibraryDialogViewModel GetAddLibraryDialogViewModel()
        {
            return new AddLibraryDialogViewModel(_commonViewModel, new LibraryRepository(_connectedServices, LibraryRepositoriesFactory), _connectedServices);
        }

        public IAddTagDialogViewModel GetAddTagDialogViewModel()
        {
            return new AddTagDialogViewModel(_commonViewModel, new CommandFactory());
        }

        public async Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages)
        {
            var viewModel = new AddImagesDialogViewModel(_commonViewModel, new LibraryRepository(_connectedServices, LibraryRepositoriesFactory), selectedImages, new CommandFactory());
            await viewModel.Initialize();

            return viewModel;
        }
    }
}
