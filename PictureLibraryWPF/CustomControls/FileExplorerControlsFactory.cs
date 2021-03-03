using Autofac;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    public class FileExplorerControlsFactory : IFileExplorerControlsFactory
    {
        private IFileExplorerViewModelFactory _fileExplorerViewModelFactory;
        private IDialogViewModelFactory _dialogViewModelFactory;

        public FileExplorerControlsFactory(IFileExplorerViewModelFactory fileExplorerViewModelFactory, IDialogViewModelFactory dialogViewModelFactory)
        {
            _fileExplorerViewModelFactory = fileExplorerViewModelFactory;
            _dialogViewModelFactory = dialogViewModelFactory;
        }

        public ElementsTree GetFileElementsTree()
        {
            return new ElementsTree(_fileExplorerViewModelFactory.GetFileTreeViewModel());
        }

        public ElementsView GetFileElementsView()
        {
            return new ElementsView(_fileExplorerViewModelFactory.GetFilesViewViewModel());
        }

        public FileExplorerToolbar GetFileExplorerToolbar()
        {
            return new FileExplorerToolbar(_fileExplorerViewModelFactory.GetFileToolboxViewModel(new WPFClipboard()), new DialogFactory(_dialogViewModelFactory));
        }
    }
}
