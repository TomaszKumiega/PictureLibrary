using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class MinimizeButtonCommand : ICommand
    {
        private IMainWindowViewModel _viewModel;

        /// <summary>
        /// Initializes new instance of <see cref="MinimizeButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel"></param>
        public MinimizeButtonCommand(IMainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
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
            _viewModel.Minimize();
        }
        #endregion
    }
}
