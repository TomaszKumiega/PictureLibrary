using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCopyCommand(IExplorerViewModel viewModel)
        {
            return new CopyCommand(viewModel);
        }

        public ICommand GetPasteCommand(IExplorerViewModel viewModel)
        {
            return new PasteCommand(viewModel);
        }

        public ICommand GetCutCommand(IExplorerViewModel viewModel)
        {
            return new CutCommand(viewModel);
        }

        public ICommand GetCopyPathCommand(IFileExplorerViewModel viewModel)
        {
            return new CopyPathCommand(viewModel);
        }

        public ICommand GetRemoveCommand(IExplorerViewModel viewModel)
        {
            return new RemoveCommand(viewModel);
        }

        public ICommand GetRenameCommand(IExplorerViewModel viewModel)
        {
            return new RenameCommand(viewModel);
        }

        public ICommand GetCreateFolderCommand(IFileExplorerViewModel viewModel)
        {
            return new CreateFolderCommand(viewModel);
        }

        public ICommand GetBackCommand(IExplorerViewModel viewModel)
        {
            return new BackCommand(viewModel);
        }

        public ICommand GetForwardCommand(IExplorerViewModel viewModel)
        {
            return new ForwardCommand(viewModel);
        }
    }
}
