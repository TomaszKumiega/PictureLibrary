using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCopyFileCommand(IExplorerViewModel viewModel)
        {
            return new CopyFileCommand(viewModel);
        }

        public ICommand GetPasteCommand(IExplorerViewModel viewModel)
        {
            return new PasteCommand(viewModel);
        }
    }
}
