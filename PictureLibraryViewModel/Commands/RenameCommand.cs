using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RenameCommand : ICommand
    {
        private IExplorerViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public RenameCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.SelectedElements == null) return false;
            else if (_viewModel.SelectedElements.Count > 0) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.RenameSelectedElements();
        }
    }
}
