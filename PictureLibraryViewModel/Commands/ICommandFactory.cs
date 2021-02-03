﻿using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCopyCommand(IExplorerViewModel viewModel);
        ICommand GetPasteCommand(IExplorerViewModel viewModel);
        ICommand GetCutCommand(IExplorerViewModel viewModel);
        ICommand GetCopyPathCommand(IFileExplorerViewModel viewModel);
        ICommand GetRemoveCommand(IExplorerViewModel viewModel);
        ICommand GetRenameCommand(IExplorerViewModel viewModel);
        ICommand GetCreateFolderCommand(IFileExplorerViewModel viewModel);
        ICommand GetBackCommand(IExplorerViewModel viewModel);
        ICommand GetForwardCommand(IExplorerViewModel viewModel);
        ICommand GetGoToParentDirectoryCommand(IFileExplorerViewModel viewModel);
        ICommand GetRefreshCommand(IExplorerViewModel viewModel);
    }
}
