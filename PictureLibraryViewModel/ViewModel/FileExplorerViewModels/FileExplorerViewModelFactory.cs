using PictureLibraryModel.Services.Clipboard;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerViewModelFactory : IFileExplorerViewModelFactory
    {
        private IFileExplorerViewModel _commonVM;

        public FileExplorerViewModelFactory(IFileExplorerViewModel commonVM)
        {
            _commonVM = commonVM;
        }

        public IFileExplorerToolboxViewModel GetFileToolboxViewModel(IClipboardService clipboard)
        {
            return new FileExplorerToolboxViewModel(_commonVM, new FileService(), new DirectoryService(), clipboard);
        }

        public IFileTreeViewModel GetFileTreeViewModel()
        {
            return new FileTreeViewModel(_commonVM);
        }

        public IFilesViewViewModel GetFilesViewViewModel()
        {
            return new FilesViewViewModel(_commonVM);
        }
    }
}
