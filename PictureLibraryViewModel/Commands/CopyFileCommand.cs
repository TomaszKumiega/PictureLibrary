using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CopyFileCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IExplorerViewModel _viewModel;

        public CopyFileCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedFile != null;
        }

        public void Execute(object parameter)
        {
            _viewModel.Copy();
        }
    }
}
