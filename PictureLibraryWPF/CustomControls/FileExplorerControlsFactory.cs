﻿using Autofac;
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
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    public class FileExplorerControlsFactory : IFileExplorerControlsFactory
    {
        private IFileExplorerViewModelFactory FileExplorerViewModelFactory { get; }
        private IDialogViewModelFactory DialogViewModelFactory { get; }

        public FileExplorerControlsFactory(IFileExplorerViewModelFactory fileExplorerViewModelFactory, IDialogViewModelFactory dialogViewModelFactory)
        {
            FileExplorerViewModelFactory = fileExplorerViewModelFactory;
            DialogViewModelFactory = dialogViewModelFactory;
        }

        public async Task<ElementsTree> GetFileElementsTreeAsync()
        {
            return new ElementsTree(await FileExplorerViewModelFactory.GetFileTreeViewModelAsync());
        }

        public async Task<ElementsView> GetFileElementsViewAsync()
        {
            return new ElementsView(await FileExplorerViewModelFactory.GetFilesViewViewModelAsync());
        }

        public FileExplorerToolbar GetFileExplorerToolbar()
        {
            return new FileExplorerToolbar(FileExplorerViewModelFactory.GetFileToolboxViewModel(new WPFClipboard()), new DialogFactory(DialogViewModelFactory));
        }
    }
}
