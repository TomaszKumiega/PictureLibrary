using PictureLibraryViewModel.ViewModel;
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
    }
}
