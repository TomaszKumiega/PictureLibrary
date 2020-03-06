using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCloseButtonCommand(IMainWindowViewModel viewModel);

        ICommand GetMaximizeButtonCommand(IMainWindowViewModel viewModel);

        ICommand GetMinimizeButtonCommand(IMainWindowViewModel viewModel);

    }
}
