using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CloseButtonCommand : ICommand
    {
        private IMainWindowViewModel viewModel;

        /// <summary>
        /// Initializes new instance of <see cref="CloseButtonCommand"/> class.
        /// </summary>
        /// <param name="viewModel"></param>
        public CloseButtonCommand(IMainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #region ICommand Members

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
            viewModel.Close();
        }

        #endregion
    }
}
