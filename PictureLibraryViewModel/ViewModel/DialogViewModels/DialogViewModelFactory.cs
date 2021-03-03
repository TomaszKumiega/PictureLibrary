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

        public DialogViewModelFactory(ILibraryExplorerViewModel commonVM, IConnectedServicesInfoProviderService connectedServices)
        {
            _commonViewModel = commonVM;
            _connectedServices = connectedServices;
        }

        public IAddLibraryDialogViewModel GetAddLibraryDialogViewModel()
        {
            return new AddLibraryDialogViewModel(_commonViewModel, new LibraryRepository(_connectedServices, new LibraryRepositoriesFactory()), _connectedServices);
        }

        public IAddTagDialogViewModel GetAddTagDialogViewModel()
        {
            return new AddTagDialogViewModel(_commonViewModel, new CommandFactory());
        }

        public async Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages)
        {
            var viewModel = new AddImagesDialogViewModel(_commonViewModel, new LibraryRepository(_connectedServices, new LibraryRepositoriesFactory()), selectedImages, new CommandFactory());
            await viewModel.Initialize();

            return viewModel;
        }
    }
}
