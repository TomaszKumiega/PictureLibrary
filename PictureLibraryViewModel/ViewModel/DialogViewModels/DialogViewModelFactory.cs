using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
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
        private ICommandFactory CommandFactory { get; }
        private IRepository<Library> LibraryRepository { get; }

        private ILibraryExplorerViewModel CommonViewModel { get; }
        private IConnectedServicesInfoProviderService ConnectedServices { get; }

        public DialogViewModelFactory(ILibraryExplorerViewModel commonVM, IConnectedServicesInfoProviderService connectedServices, 
            IImageProviderService imageProviderService, ICommandFactory commandFactory, IRepository<Library> libraryRepository)
        {
            CommonViewModel = commonVM;
            ConnectedServices = connectedServices;
            ImageProviderService = imageProviderService;
            CommandFactory = commandFactory;
            LibraryRepository = libraryRepository;

        }

        public IAddLibraryDialogViewModel GetAddLibraryDialogViewModel()
        {
            return new AddLibraryDialogViewModel(CommonViewModel, LibraryRepository, ConnectedServices, CommandFactory);
        }

        public IAddTagDialogViewModel GetAddTagDialogViewModel()
        {
            return new AddTagDialogViewModel(CommonViewModel, CommandFactory);
        }

        public async Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages)
        {
            var viewModel = new AddImagesDialogViewModel(CommonViewModel, LibraryRepository, selectedImages, CommandFactory, ImageProviderService);
            await viewModel.Initialize();

            return viewModel;
        }
    }
}
