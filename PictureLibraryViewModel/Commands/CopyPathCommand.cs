using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CopyPathCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IFileExplorerViewModel _viewModel;

        public CopyPathCommand(IFileExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.SelectedElements == null) return false;
            else if (_viewModel.SelectedElements.Any()) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.CopyPath();
        }
    }
}
