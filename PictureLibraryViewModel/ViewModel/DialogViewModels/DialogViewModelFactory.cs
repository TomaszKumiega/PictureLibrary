using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories.LibraryRepositories;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using PictureLibraryModel.Services.ImageProviderService;
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
        private IImageProviderService ImageProviderService { get; }

        private ILibraryExplorerViewModel CommonViewModel { get; }
        private IConnectedServicesInfoProviderService ConnectedServices { get; }
        private ILibraryRepositoriesFactory LibraryRepositoriesFactory { get; }

        public DialogViewModelFactory(ILibraryExplorerViewModel commonVM, IConnectedServicesInfoProviderService connectedServices, ILibraryRepositoriesFactory libraryRepositoriesFactory, 
            IImageProviderService imageProviderService)
        {
            CommonViewModel = commonVM;
            ConnectedServices = connectedServices;
            LibraryRepositoriesFactory = libraryRepositoriesFactory;
            ImageProviderService = imageProviderService;

        }

        public IAddLibraryDialogViewModel GetAddLibraryDialogViewModel()
        {
            return new AddLibraryDialogViewModel(CommonViewModel, new LibraryRepository(ConnectedServices, LibraryRepositoriesFactory), ConnectedServices, new CommandFactory());
        }

        public IAddTagDialogViewModel GetAddTagDialogViewModel()
        {
            return new AddTagDialogViewModel(CommonViewModel, new CommandFactory());
        }

        public async Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages)
        {
            var viewModel = new AddImagesDialogViewModel(CommonViewModel, new LibraryRepository(ConnectedServices, LibraryRepositoriesFactory), selectedImages, new CommandFactory(), ImageProviderService);
            await viewModel.Initialize();

            return viewModel;
        }
    }
}
