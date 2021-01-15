using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CopyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IExplorerViewModel _viewModel;

        public CopyCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.SelectedFiles == null) return false;
            else if (_viewModel.SelectedFiles.Any()) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.Copy();
        }
    }
}
