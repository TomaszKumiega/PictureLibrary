using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCopyCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetPasteCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetCutCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetCopyPathCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetRemoveCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetRenameCommand(IExplorerToolboxViewModel viewModel);
        ICommand GetCreateFolderCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetBackCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetForwardCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetGoToParentDirectoryCommand(IFileExplorerToolboxViewModel viewModel);
        ICommand GetRefreshCommand(IExplorerToolboxViewModel viewModel);
    }
}
