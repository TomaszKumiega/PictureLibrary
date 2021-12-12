using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerViewModelFactory : IFileExplorerViewModelFactory
    {
        private IFileExplorerViewModel CommonVM { get; }
        private IDirectoryService DirectoryService { get; }

        public FileExplorerViewModelFactory(IFileExplorerViewModel commonVM, IDirectoryService directoryService)
        {
            CommonVM = commonVM;
            DirectoryService = directoryService;
        }

        public IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard)
        {
            return new FileExplorerToolboxViewModel(CommonVM, new FileService(), DirectoryService, clipboard, new CommandFactory());
        }

        public async Task<IExplorableElementsTreeViewModel> GetFileTreeViewModelAsync()
        {
            var viewModel = new FileTreeViewModel(CommonVM, DirectoryService);
            await viewModel.InitializeDirectoryTreeAsync();
            return viewModel;
        }

        public async Task<IExplorableElementsViewViewModel> GetFilesViewViewModelAsync()
        { 
            await CommonVM.LoadCurrentlyShownElementsAsync();
            return new FilesViewViewModel(CommonVM);
        }
    }
}
