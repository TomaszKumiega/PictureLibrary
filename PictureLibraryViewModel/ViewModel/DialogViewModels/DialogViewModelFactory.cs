using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    //TODO: REFACTOR
    public class DialogViewModelFactory : IDialogViewModelFactory
    {
        private ICommandFactory CommandFactory { get; }
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private IDataSourceCollection DataSourceCollection { get; }

        public DialogViewModelFactory(ILibraryExplorerViewModel commonVM, ICommandFactory commandFactory, IDataSourceCollection dataSourceCollection)
        {
            CommonViewModel = commonVM;
            CommandFactory = commandFactory;
            DataSourceCollection = dataSourceCollection;

            DataSourceCollection.Initialize(new List<IRemoteStorageInfo>());
        }

        public IAddTagDialogViewModel GetAddTagDialogViewModel()
        {
            return new AddTagDialogViewModel(CommonViewModel, CommandFactory);
        }

        public async Task<IAddImagesDialogViewModel> GetImagesDialogViewModel(List<ImageFile> selectedImages)
        {
            var viewModel = new AddImagesDialogViewModel(CommonViewModel, DataSourceCollection, selectedImages, CommandFactory);
            await viewModel.Initialize();

            return viewModel;
        }
    }
}
