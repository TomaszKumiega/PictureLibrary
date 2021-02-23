using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCopyCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new CopyCommand(viewModel);
        }

        public ICommand GetPasteCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new PasteCommand(viewModel);
        }

        public ICommand GetCutCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new CutCommand(viewModel);
        }

        public ICommand GetCopyPathCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new CopyPathCommand(viewModel);
        }

        public ICommand GetRemoveCommand(IExplorerToolboxViewModel viewModel)
        {
            return new RemoveCommand(viewModel);
        }

        public ICommand GetRenameCommand(IExplorerToolboxViewModel viewModel)
        {
            return new RenameCommand(viewModel);
        }

        public ICommand GetCreateFolderCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new CreateFolderCommand(viewModel);
        }

        public ICommand GetBackCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new BackCommand(viewModel);
        }

        public ICommand GetForwardCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new ForwardCommand(viewModel);
        }

        public ICommand GetGoToParentDirectoryCommand(IFileExplorerToolboxViewModel viewModel)
        {
            return new GoToParentDirectoryCommand(viewModel);
        }

        public ICommand GetRefreshCommand(IExplorerToolboxViewModel viewModel)
        {
            return new RefreshCommand(viewModel);
        }
    }
}
