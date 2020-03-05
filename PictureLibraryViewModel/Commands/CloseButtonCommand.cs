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
        private MainWindowViewModel viewModel;

        public CloseButtonCommand(MainWindowViewModel viewModel)
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
