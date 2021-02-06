using Autofac;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    public class MainWindowControlsFactory : IMainWindowControlsFactory
    {
        private IFilesViewViewModel _filesViewViewModel;
        private IFileExplorerToolboxViewModel _filesToolboxViewModel;
        private IFileTreeViewModel _filesTreeViewModel;

        public MainWindowControlsFactory(IFileExplorerViewModelFactory factory)
        {
            _filesViewViewModel = factory.GetFilesViewViewModel();
            _filesToolboxViewModel = factory.GetFileToolboxViewModel(new WPFClipboard());
            _filesTreeViewModel = factory.GetFileTreeViewModel();
        }

        public ElementsTree GetFileElementsTree()
        {
            return new ElementsTree(_filesTreeViewModel);
        }

        public ElementsView GetFileElementsView()
        {
            return new ElementsView(_filesViewViewModel);
        }

        public FileExplorerToolbar GetFileExplorerToolbar()
        {
            return new FileExplorerToolbar(_filesToolboxViewModel);
        }
    }
}
