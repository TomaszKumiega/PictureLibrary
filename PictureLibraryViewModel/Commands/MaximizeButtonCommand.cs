using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class MaximizeButtonCommand : ICommand
    {
        private IMainWindowViewModel ViewModel { get; }

        /// <summary>
        /// Initializes new instance of <see cref="MaximizeButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel"></param>
        public MaximizeButtonCommand(IMainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        #region ICommand members
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Maximize();
        }
        #endregion
    }
}
