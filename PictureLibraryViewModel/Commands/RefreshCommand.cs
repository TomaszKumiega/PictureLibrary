using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RefreshCommand : ICommand
    {
        private IExplorerViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public RefreshCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.Refresh();
        }
    }
}
