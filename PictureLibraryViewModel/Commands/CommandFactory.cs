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
    }
}
