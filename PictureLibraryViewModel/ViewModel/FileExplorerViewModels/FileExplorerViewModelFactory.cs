using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    //TODO: remove
    public class FileExplorerViewModelFactory : IFileExplorerViewModelFactory
    {
        private IFileExplorerViewModel CommonVM { get; }
        private IDirectoryService DirectoryService { get; }
        private ICommandCreator _commandCreator;

        public FileExplorerViewModelFactory(IFileExplorerViewModel commonVM, ICommandCreator commandCreator, IDirectoryService directoryService)
        {
            CommonVM = commonVM;
            DirectoryService = directoryService;
            _commandCreator = commandCreator;
        }

        public IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard)
        {
            return new FileExplorerToolboxViewModel(CommonVM, new FileService(), DirectoryService, clipboard, _commandCreator);
        }

        public async Task<IExplorableElementsTreeViewModel> GetFileTreeViewModelAsync()
        {
            var viewModel = new FileTreeViewModel(CommonVM, DirectoryService);
            await viewModel.InitializeDirectoryTreeAsync();
            return viewModel;
        }

        public async Task<IExplorableElementsViewViewModel> GetFilesViewViewModelAsync()
        { 
            //await CommonVM.LoadCurrentlyShownElementsAsync();
            return new FilesViewViewModel(CommonVM);
        }
    }
}
