﻿using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCopyCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetPasteCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetCutCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetCopyPathCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetRemoveCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetRenameCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetCreateFolderCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetBackCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetForwardCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetGoToParentDirectoryCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetRefreshCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetAddTagCommand(IAddTagDialogViewModel viewModel);
        ICommand GetAddImagesCommand(IAddImagesDialogViewModel viewModel);
        ICommand GetAddLibraryCommand(IAddLibraryDialogViewModel viewModel);
    }
}
