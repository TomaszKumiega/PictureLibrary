using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    /// <summary>
    /// Allows instantiation of diffrent commands.
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Creates new instance of <see cref="CloseButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        ICommand GetCloseButtonCommand(IMainWindowViewModel viewModel);

        /// <summary>
        /// Creates new instance of <see cref="MaximizeButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        ICommand GetMaximizeButtonCommand(IMainWindowViewModel viewModel);

        /// <summary>
        /// Creates new instance of <see cref="MinimizeButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        ICommand GetMinimizeButtonCommand(IMainWindowViewModel viewModel);

    }
}
