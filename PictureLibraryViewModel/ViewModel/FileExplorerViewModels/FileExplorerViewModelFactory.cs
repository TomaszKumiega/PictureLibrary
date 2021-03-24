using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerViewModelFactory : IFileExplorerViewModelFactory
    {
        private IFileExplorerViewModel CommonVM { get; }

        public FileExplorerViewModelFactory(IFileExplorerViewModel commonVM)
        {
            CommonVM = commonVM;
        }

        public IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard)
        {
            return new FileExplorerToolboxViewModel(CommonVM, new FileService(), new DirectoryService(), clipboard, new CommandFactory());
        }

        public async Task<IExplorableElementsTreeViewModel> GetFileTreeViewModelAsync()
        {
            var viewModel = new FileTreeViewModel(CommonVM, new DirectoryService());
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
