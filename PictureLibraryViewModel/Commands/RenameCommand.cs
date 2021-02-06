using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RenameCommand : ICommand
    {
        private IExplorerToolboxViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public RenameCommand(IExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CommonViewModel.SelectedElements == null) return false;
            else if (_viewModel.CommonViewModel.SelectedElements.Count > 0) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.Rename();
        }
    }
}
