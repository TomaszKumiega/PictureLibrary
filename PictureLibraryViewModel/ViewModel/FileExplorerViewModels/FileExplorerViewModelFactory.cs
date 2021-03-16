using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IExplorableElementsTreeViewModel GetFileTreeViewModel()
        {
            return new FileTreeViewModel(CommonVM, new DirectoryService());
        }

        public IExplorableElementsViewViewModel GetFilesViewViewModel()
        {
            return new FilesViewViewModel(CommonVM);
        }
    }
}
