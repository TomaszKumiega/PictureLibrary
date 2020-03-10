using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;

namespace PictureLibraryViewModel.Commands
{
    /// <summary>
    /// <inheritdoc cref="ICommandFactory"/>
    /// </summary>
    public class CommandFactory : ICommandFactory
    { 
        public ICommand GetCloseButtonCommand(IMainWindowViewModel viewModel)
        {
            return new CloseButtonCommand(viewModel);
        }
    }
}
